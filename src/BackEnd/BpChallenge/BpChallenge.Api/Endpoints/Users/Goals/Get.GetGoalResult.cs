using BpChallenge.Api.DTOs;

namespace BpChallenge.Api.Endpoints.Users.Goals;

public record GetGoalResult(string GoalTitle,
                              int Years,
                              double InitialInvestment,
                              double MonthlyContribution,
                              double TargetAmount,
                              GoalCategoryResult GoalCategory,
                              FinancialEntityResult FinancialEntity,
                              double TotalContributions,
                              double TotalWithdrawal,
                              string GoalAchievementPercentage);