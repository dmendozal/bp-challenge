using System;

namespace BpChallenge.Domain.Entities;

public class CurrencyIndicator : BaseEntity
{
    public int SourceCurrencyId { get; private set; }
    public Currency SourceCurrency { get; private set; }
    public int DestinationCurrencyId { get; private set; }
    public Currency DestinationCurrency { get; private set; }
    public double Value { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime RequestDate { get; private set; }
    public double Ask { get; private set; }
    public double Bid { get; private set; }
}
