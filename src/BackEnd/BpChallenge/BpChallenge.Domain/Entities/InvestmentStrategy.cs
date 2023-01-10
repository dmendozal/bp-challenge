namespace BpChallenge.Domain.Entities;

public class InvestmentStrategy : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Code { get; private set; }
    public int FinancialEntityId { get; private set; }
    public FinancialEntity FinancialEntity { get; private set; }
    public bool IsVisible { get; private set; }
    public bool IsDefault { get; private set; }
    public string ShortTitle { get; private set; }
    public int InvestmentStrategyTypeId { get; private set; }
    public InvestmentStrategyType InvestmentStrategyType { get; private set; }
    public string IconUrl { get; private set; }
    public bool IsRecomended { get; private set; }
    public string RecomendedFor { get; private set; }
    public int DisplayOrder { get; private set; }
}
