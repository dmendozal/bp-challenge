namespace BpChallenge.Domain.Entities;

public class PortafolioFunding : BaseEntity
{
    public decimal Percentage { get; private set; }
    public int FundingId { get; private set; }
    public Funding Funding { get; private set; }
    public int PortafolioId { get; private set; }
    public Portafolio Portafolio { get; private set; }
}
