using BpChallenge.Api.DTOs;
using System;

namespace BpChallenge.Api.Endpoints.Users.Goals;

public record ListGoalResult(string GoalTitle,
                              int Years,
                              double InitialInvestment,
                              double MonthlyContribution,
                              double TargetAmount,
                              FinancialEntityResult FinancialEntity,
                              PortfolioResult Portfolio,
                              DateTime CreationDate);


