using Ardalis.ApiEndpoints;
using BpChallenge.Api.DTOs;
using BpChallenge.Api.Exceptions;
using BpChallenge.Domain.Entities;
using BpChallenge.Infrastructure.Persistence;
using BpChallenge.Infrastructure.Persistence.Repositories.Interfaces;
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
    private readonly ISummaryRepository _summaryRepository;

    public Get(BpChallengeContextDb bpChallengeContextDb, ISummaryRepository summaryRepository)
    {
        _dbContext = bpChallengeContextDb;
        _summaryRepository = summaryRepository;
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
            return NotFound(ErrorMessages.UserNotExists);

        var goal = await _dbContext.Set<Goal>()
                                    .Include(x => x.FinancialEntity)
                                    .Include(x => x.GoalCategory)
                                    .Include(x => x.Portfolio)
                                    .FirstOrDefaultAsync(x => x.Id == command.GoalId && x.UserId == user.Id,
                                                         cancellationToken: cancellationToken);

        if (goal == null)
            return NotFound(ErrorMessages.GoalUserNotExists);

        var transactions = await _dbContext.Set<GoalTransaction>()
                                           .Where(x => x.GoalId == goal.Id)
                                           .ToListAsync(cancellationToken: cancellationToken);

        var goalTransactionFunding = await _dbContext.Set<GoalTransactionFunding>()
                                                     .Include(x => x.Funding)
                                                     .Where(x => x.GoalId == goal.Id)
                                                     .ToListAsync(cancellationToken: cancellationToken);

        var fundingShareValues = await _dbContext.Set<FundingShareValue>().ToListAsync(cancellationToken: cancellationToken);
        var balance = await _summaryRepository.GetBalance(goalTransactionFunding, user.CurrencyId, fundingShareValues);
        var goalAchievementPercentage = _summaryRepository.GetGoalAchievementPercentage(goal, balance);

        var result = new GetGoalResult(goal.Title,
                                       goal.Years,
                                       goal.InitialInvestment,
                                       goal.MonthlyContribution,
                                       goal.TargetAmount,
                                       new GoalCategoryResult(goal.GoalCategory.Title, goal.GoalCategory.Code),
                                       new FinancialEntityResult(goal.FinancialEntity.Title, goal.FinancialEntity.Description, goal.FinancialEntity.Logo),
                                       transactions.Where(x => x.Type == "buy").Sum(x => x.Amount),
                                       transactions.Where(x => x.Type == "sale").Sum(x => x.Amount),
                                       goalAchievementPercentage);

        return Ok(result);
    }
}
