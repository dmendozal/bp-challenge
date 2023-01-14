using Microsoft.AspNetCore.Mvc;

namespace BpChallenge.Api.Endpoints.Users.Goals;

public record GetGoalCommand
{
    [FromRoute(Name = "userId")] public int UserId { get; set; }
    [FromRoute(Name = "goalId")] public int GoalId { get; set; }
}