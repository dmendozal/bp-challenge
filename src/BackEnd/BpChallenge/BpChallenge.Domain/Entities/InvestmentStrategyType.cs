namespace BpChallenge.Domain.Entities;

public class InvestmentStrategyType : BaseEntity
{
    public string IconUrl { get; private set; }
    public string Title { get; private set; }
    public string ShortTitle { get; private set; }
    public string Description { get; private set; }
    public string RecommendedFor { get; private set; }
    public bool IsVisible { get; private set; }
    public bool IsDefault { get; private set; }
    public int DisplayOrder { get; private set; }

    public int FinancialEntityId { get; private set; }
    public FinancialEntity FinancialEntity { get; private set; }
}
