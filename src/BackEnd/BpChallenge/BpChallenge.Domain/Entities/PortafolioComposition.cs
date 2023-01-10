namespace BpChallenge.Domain.Entities;

public class PortafolioComposition : BaseEntity
{
    public decimal Percentage { get; private set; }
    public int CompositionSubCategoryId { get; private set; }
    public CompositionSubCategory CompositionSubCategory { get; private set; }
    public int PortafolioId { get; private set; }
    public Portafolio Portafolio { get; private set; }
}
