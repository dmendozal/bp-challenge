using System;

namespace BpChallenge.Domain.Entities;

public class CurrencyIndicator : BaseEntity
{
    public int SourceCurrencyId { get; private set; }
    public Currency SourceCurrency { get; private set; }
    public int DestinationCurrencyId { get; private set; }
    public Currency DestinationCurrency { get; private set; }
    public decimal Value { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime RequestDate { get; private set; }
    public decimal Ask { get; private set; }
    public decimal Bid { get; private set; }
}
