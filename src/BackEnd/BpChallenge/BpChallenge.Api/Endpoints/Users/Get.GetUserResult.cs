using System;

namespace BpChallenge.Api.Endpoints.Users;

public record GetUserResult(int Id, string FullName, string AdvisorFullName, DateTime CreatedAt);
