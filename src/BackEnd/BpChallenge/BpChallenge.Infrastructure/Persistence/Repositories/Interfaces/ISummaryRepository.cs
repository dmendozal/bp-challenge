using BpChallenge.Domain.Entities;

namespace BpChallenge.Infrastructure.Persistence.Repositories.Interfaces;

public interface ISummaryRepository
{
    public Task<double> GetBalance(List<GoalTransactionFunding> goalTransactionFunding, int currencyId, List<FundingShareValue> fundingShareValues);
    public string GetGoalAchievementPercentage(Goal goal, double balance);
}
