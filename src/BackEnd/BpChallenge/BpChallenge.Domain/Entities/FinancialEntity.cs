namespace BpChallenge.Domain.Entities;

public class FinancialEntity : BaseEntity
{
    public string Logo { get; private set; }
    public string Title { get; private set; }
    public string Uuid { get; private set; }
    public string Description { get; private set; }
    public int DefaultCurrencyId { get; private set; }
    public Currency DefaultCurrency { get; private set; }
}
