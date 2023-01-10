using Ardalis.ApiEndpoints;
using BpChallenge.Domain.Entities;
using BpChallenge.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace BpChallenge.Api.Endpoints.Users;

public class Get : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<GetUserResult>
{
    private readonly BpChallengeContextDb _dbContext;

    public Get(BpChallengeContextDb bpChallengeContextDb)
    {
        _dbContext = bpChallengeContextDb;
    }


    [HttpGet("api/users/{userId:int}")]
    [SwaggerOperation(
        Summary = "Get User Information",
        Description = "Get user information from database",
        OperationId = "User.Get",
        Tags = new[] { "Users" })
    ]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public override async Task<ActionResult<GetUserResult>> HandleAsync(int userId, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
            return NotFound("User does not exists.");

        return Ok(new GetUserResult(user.Id,
                                    $"{user.FirstName} {user.Surname}",
                                    $"{user.Advisor.FirstName} {user.Advisor.Surname}",
                                    user.Created));
    }
}
