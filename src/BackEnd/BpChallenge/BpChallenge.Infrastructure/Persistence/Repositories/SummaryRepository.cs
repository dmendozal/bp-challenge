using BpChallenge.Domain.Entities;
using BpChallenge.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BpChallenge.Infrastructure.Persistence.Repositories;

public class SummaryRepository : ISummaryRepository
{
    private readonly BpChallengeContextDb _dbContext;
    public SummaryRepository(BpChallengeContextDb dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<double> GetBalance(List<GoalTransactionFunding> goalTransactionFunding, int currencyId, List<FundingShareValue> fundingShareValues)
    {
        var balance = new List<double>();

        foreach (var item in goalTransactionFunding)
        {
            double result = default;
            var quotaValue = item.Funding.IsBox ? 1 : item.Quotas;

            if (item.Funding.HasShareValue)
            {
                var fundingShareValue = fundingShareValues.FirstOrDefault(x => x.Date == item.Date && x.FundingId == item.FundingId);
                result = quotaValue * fundingShareValue!.Value;
            }

            var currencyIndicatorValue = await GetCurrencyIndicatorValue(item.Transaction.CurrencyId, currencyId, item.Date);
            result = quotaValue * currencyIndicatorValue;

            balance.Add(result);
        }

        return balance.Sum();
    }

    public string GetGoalAchievementPercentage(Goal goal, double balance)
    {
        return (balance / goal.TargetAmount).ToString("P");
    }

    private async Task<double> GetCurrencyIndicatorValue(int currencySourceId, int currencyDestineId, DateTime date)
    {
        var currencyIndicator = await _dbContext.Set<CurrencyIndicator>()
                                          .FirstOrDefaultAsync(x => x.SourceCurrencyId == currencySourceId
                                                                    && x.DestinationCurrencyId == currencyDestineId
                                                                    && x.Date == date);
        return currencyIndicator?.Value ?? 1;
    }
}
