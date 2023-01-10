namespace BpChallenge.Domain.Entities;

public class FundingComposition : BaseEntity
{
    public decimal Percentage { get; private set; }
    public int FundingId { get; private set; }
    public Funding Funding { get; private set; }

    public int SubCategoryId { get; private set; }
    public CompositionSubCategory SubCategory { get; private set;}
}
