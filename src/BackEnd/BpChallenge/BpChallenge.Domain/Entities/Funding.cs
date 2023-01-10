namespace BpChallenge.Domain.Entities;

public class Funding : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string UuId { get; private set; }
    public string Mnemonic { get; private set; }

    public int FinancialEntityId { get; private set; }
    public FinancialEntity FinancialEntity { get; private set; }

    public bool IsBox { get; private set; }
    public string YahooMnemonic { get; private set; }
    public string CmFurl { get; private set; }
    public bool HasShareValue { get; private set; }

    public int CurrencyId { get; private set; }
    public Currency Currency { get; private set; }
}