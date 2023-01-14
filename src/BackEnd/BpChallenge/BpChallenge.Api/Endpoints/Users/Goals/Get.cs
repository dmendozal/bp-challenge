using Ardalis.ApiEndpoints;
using BpChallenge.Api.DTOs;
using BpChallenge.Domain.Entities;
using BpChallenge.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BpChallenge.Api.Endpoints.Users.Goals;

public class Get : EndpointBaseAsync
    .WithRequest<GetGoalCommand>
    .WithActionResult<GetGoalResult>
{
    private readonly BpChallengeContextDb _dbContext;

    public Get(BpChallengeContextDb bpChallengeContextDb)
    {
        _dbContext = bpChallengeContextDb;
    }

    [HttpGet("api/users/{userId:int}/goals/{goalId:int}")]
    [SwaggerOperation(
        Summary = "Get Goal Detail by User",
        Description = "Get goal information by user from database",
        OperationId = "User.Goal",
        Tags = new[] { "Users" })
    ]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public override async Task<ActionResult<GetGoalResult>> HandleAsync([FromRoute] GetGoalCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Set<User>()
            .Include(x => x.Currency)
            .FirstOrDefaultAsync(x => x.Id == command.UserId, cancellationToken: cancellationToken);

        if (user == null)
            return NotFound("El usuario no existe.");

        var goal = await _dbContext.Set<Goal>()
                                    .Include(x => x.FinancialEntity)
                                    .Include(x => x.GoalCategory)
                                    .Include(x => x.Portfolio)
                                    .FirstOrDefaultAsync(x => x.Id == command.GoalId && x.UserId == user.Id, cancellationToken: cancellationToken);

        if (goal == null)
            return NotFound("El usuario no tiene una meta asignada");

        var transactions = await _dbContext.Set<GoalTransaction>()
                                           .Where(x => x.GoalId == goal.Id)
                                           .ToListAsync(cancellationToken: cancellationToken);

        

        var result = new GetGoalResult(goal.Title,
                                       goal.Years,
                                       goal.InitialInvestment,
                                       goal.MonthlyContribution,
                                       goal.TargetAmount,
                                       new GoalCategoryResult(goal.GoalCategory.Title, goal.GoalCategory.Code),
                                       new FinancialEntityResult(goal.FinancialEntity.Title, goal.FinancialEntity.Description, goal.FinancialEntity.Logo),
                                       transactions.Where(x => x.Type == "buy").Sum(x => x.Amount),
                                       transactions.Where(x => x.Type == "sale").Sum(x => x.Amount),
                                       0);



        return Ok(result);
    }
}
