using Ardalis.ApiEndpoints;
using BpChallenge.Api.DTOs;
using BpChallenge.Domain.Entities;
using BpChallenge.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BpChallenge.Api.Endpoints.Users.Goals;

public class List : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<IEnumerable<ListGoalResult>>
{
    private readonly BpChallengeContextDb _dbContext;

    public List(BpChallengeContextDb bpChallengeContextDb)
    {
        _dbContext = bpChallengeContextDb;
    }

    [HttpGet("api/users/{userId:int}/goals")]
    [SwaggerOperation(
        Summary = "List User Goals",
        Description = "List user goals from database",
        OperationId = "User.Goals",
        Tags = new[] { "Users" })
    ]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public override async Task<ActionResult<IEnumerable<ListGoalResult>>> HandleAsync(int userId, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Set<User>()
            .Include(x => x.Currency)
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken);

        if (user == null)
            return NotFound("User does not exists.");

        var goals = await _dbContext.Set<Goal>()
                                    .Include(x => x.FinancialEntity)
                                    .Include(x => x.Portfolio)
                                    .Where(x => x.UserId == user.Id)
                                    .ToListAsync(cancellationToken: cancellationToken);

        var result = goals.Select(x => new ListGoalResult(
            x.Title,
            x.Years,
            x.InitialInvestment,
            x.MonthlyContribution,
            x.TargetAmount,
            new FinancialEntityResult(x.FinancialEntity.Title, x.FinancialEntity.Description, x.FinancialEntity.Logo),
            new PortfolioResult(x.Portfolio.Title, x.Portfolio.Description, x.Portfolio.MaxRangeYear, x.Portfolio.MinRangeYear),
            x.Created.Date)).ToList();

        return Ok(result);
    }
}
