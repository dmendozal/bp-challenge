using Ardalis.ApiEndpoints;
using BpChallenge.Domain.Entities;
using BpChallenge.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
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
        Summary = "Get User Summary Balance",
        Description = "Get user summary balance from database",
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


        return Ok(new GetSummaryResult(0, $"{contributions} {currency}"));
    }
}
