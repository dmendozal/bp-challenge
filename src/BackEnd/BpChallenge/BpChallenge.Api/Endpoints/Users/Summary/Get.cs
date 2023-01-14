using Ardalis.ApiEndpoints;
using BpChallenge.Api.Exceptions;
using BpChallenge.Domain.Entities;
using BpChallenge.Infrastructure.Persistence;
using BpChallenge.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BpChallenge.Api.Endpoints.Users.Summary;

public class Get : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<IEnumerable<GetSummaryResult>>
{
    private readonly BpChallengeContextDb _dbContext;
    private readonly ISummaryRepository _summaryRepository;

    public Get(BpChallengeContextDb bpChallengeContextDb, ISummaryRepository summaryRepository)
    {
        _dbContext = bpChallengeContextDb;
        _summaryRepository = summaryRepository;
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
    public override async Task<ActionResult<IEnumerable<GetSummaryResult>>> HandleAsync(int userId, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Set<User>()
            .Include(x => x.Currency)
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken);

        if (user == null)
            return NotFound(ErrorMessages.UserNotExists);

        var summaryResult = new List<GetSummaryResult>();
        var currency = user.Currency.Name;

        var goalTransactionFunding = await _dbContext.Set<GoalTransactionFunding>()
                                              .Include(x => x.Goal)
                                              .Include(x => x.Transaction)
                                              .Include(x => x.Funding)
                                              .Where(x => x.OwnerId == userId)
                                              .GroupBy(x => x.Goal.Title)
                                              .ToListAsync(cancellationToken: cancellationToken);

        var fundingShareValues = await _dbContext.Set<FundingShareValue>().ToListAsync(cancellationToken: cancellationToken);

        foreach (var item in goalTransactionFunding)
        {
            if (item.Any())
            {
                var balance = await _summaryRepository.GetBalance(item.ToList(), user.CurrencyId, fundingShareValues);

                summaryResult.Add(new GetSummaryResult(item.Key,
                                                  $"{balance} {currency}",
                                                  $"{item.Sum(x => x.Amount)} {currency}"));
            }
        }

        return Ok(summaryResult);
    }
}
