using System;

namespace BpChallenge.Domain.Entities;

public class FundingShareValue : BaseEntity
{
    public double Value { get; private set; }
    public DateTime Date { get; private set; }
    public int FundingId { get; private set; }
    public Funding Funding { get; private set; }
}
