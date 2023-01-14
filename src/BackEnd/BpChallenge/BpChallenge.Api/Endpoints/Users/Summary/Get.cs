using Ardalis.ApiEndpoints;
using BpChallenge.Domain.Entities;
using BpChallenge.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BpChallenge.Api.Endpoints.Users.Summary;

public class Get : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<GetSummaryResult>
{
    private readonly BpChallengeContextDb _dbContext;

    public Get(BpChallengeContextDb bpChallengeContextDb)
    {
        _dbContext = bpChallengeContextDb;
    }

    [HttpGet("api/users/{userId:int}/summary")]
    [SwaggerOperation(
        Summary = "Get User Summary",
        Description = "Get user summary from database",
        OperationId = "User.Summary",
        Tags = new[] { "Users" })
    ]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public override async Task<ActionResult<GetSummaryResult>> HandleAsync(int userId, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Set<User>()
            .Include(x => x.Currency)
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken);

        if (user == null)
            return NotFound("User does not exists.");

        var currency = user.Currency.Name;
        var contributions = await _dbContext.Set<GoalTransaction>()
                                            .Where(x => x.Type == "buy" && x.OwnerId == userId)
                                            .SumAsync(x => x.Amount, cancellationToken: cancellationToken);
        var balance = await GetBalance(userId);

        return Ok(new GetSummaryResult($"{balance} {currency}", $"{contributions} {currency}"));
    }

    private async Task<double> GetBalance(int userId)
    {
        var listResult = new List<double>();
        var fundingShareValues = await _dbContext.Set<FundingShareValue>().ToListAsync();
        var goalTransactionFunding = await _dbContext.Set<GoalTransactionFunding>()
                                                     .Include(x => x.Funding)
                                                     .Include(x => x.Transaction)
                                                     .Where(x => x.OwnerId == userId)
                                                     .ToListAsync();
        if (goalTransactionFunding.Count > 0)
        {
            foreach (var item in goalTransactionFunding)
            {
                double result = default;
                var quotaValue = item.Funding.IsBox ? 1 : item.Quotas;

                if (item.Funding.HasShareValue)
                {
                    var fundingShareValue = fundingShareValues.FirstOrDefault(x => x.Date == item.Date && x.FundingId == item.FundingId);
                    result = quotaValue * fundingShareValue.Value;
                }

                var currencyIndicatorValue = await GetCurrencyIndicatorValue(item.Transaction.CurrencyId, item.Funding.CurrencyId, item.Date);

                result = quotaValue * currencyIndicatorValue;

                listResult.Add(result);
            }
        }

        return listResult.Sum();
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
