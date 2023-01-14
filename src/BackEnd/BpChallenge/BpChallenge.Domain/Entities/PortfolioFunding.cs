namespace BpChallenge.Domain.Entities;

public class PortfolioFunding : BaseEntity
{
    public decimal Percentage { get; private set; }
    public int FundingId { get; private set; }
    public Funding Funding { get; private set; }
    public int PortfolioId { get; private set; }
    public Portfolio Portfolio { get; private set; }
}
