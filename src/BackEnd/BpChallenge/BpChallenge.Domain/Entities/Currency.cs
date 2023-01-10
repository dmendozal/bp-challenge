namespace BpChallenge.Domain.Entities;

public class Currency : BaseEntity
{
    public string Name { get; private set; }
    public string UuId { get; private set; }
    public string YahoomNemonic { get; private set; }
    public string CurrencyCode { get; private set; }
    public string DigitsInfo { get; private set; }
    public string Display { get; private set; }
    public string Locale { get; private set; }
    public string ServerFormatNumber { get; private set; }
}
