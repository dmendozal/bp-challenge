namespace BpChallenge.Domain.Entities;

public class CompositionSubCategory : BaseEntity
{
    public string Name { get; private set; }
    public string UuId { get; private set; }
    public string Description { get; private set; }

    public int CategoryId { get; private set; }
    public CompositionCategory Category { get; private set; }
}
