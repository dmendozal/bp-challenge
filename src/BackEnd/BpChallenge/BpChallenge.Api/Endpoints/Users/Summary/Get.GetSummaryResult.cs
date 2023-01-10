using System;
namespace BpChallenge.Api.Endpoints.Users.Summary;

public record GetSummaryResult(decimal Balance, string Contributions);
