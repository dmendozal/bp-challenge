namespace BpChallenge.Domain.Entities;

public class Portafolio : BaseEntity
{
    public decimal MaxRangeYear { get; private set; }
    public decimal MinRangeYear { get; private set; }
    public string UuId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

    public int FinancialEntityId { get; private set; }
    public FinancialEntity FinancialEntity { get; private set; }

    public int RiskLevelId { get; private set; }
    public RiskLevel RiskLevel { get; private set; }

    public bool IsDefault { get; private set; }
    public string Profitability { get; private set; }
    public int InvestmentStrategyId { get; private set; }
    public InvestmentStrategy InvestmentStrategy { get; private set; }
    public string Version { get; private set; }
    public string ExtraProfitabilityCurrencyCode { get; private set; }
    public decimal EstimatedProfitability { get; private set; }
    public decimal BpComission { get; private set; }
}
