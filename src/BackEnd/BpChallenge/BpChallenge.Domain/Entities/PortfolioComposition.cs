namespace BpChallenge.Domain.Entities;

public class PortfolioComposition : BaseEntity
{
    public decimal Percentage { get; private set; }
    public int CompositionSubCategoryId { get; private set; }
    public CompositionSubCategory CompositionSubCategory { get; private set; }
    public int PortfolioId { get; private set; }
    public Portfolio Portfolio { get; private set; }
}
