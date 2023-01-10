namespace BpChallenge.Domain.Entities;

public class GoalCategory : BaseEntity
{
    public string Code { get; private set; }
    public string UuId { get; private set; }
    public string Title { get; private set; }
}
