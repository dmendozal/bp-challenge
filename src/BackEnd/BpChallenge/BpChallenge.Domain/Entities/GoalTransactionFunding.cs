using System;

namespace BpChallenge.Domain.Entities;

public class GoalTransactionFunding : BaseEntity
{
    public decimal Percentage { get; private set; }
    public decimal Amount { get; private set; }
    public decimal Quotas { get; private set; }
    public DateTime Date { get; private set; }
    public int FundingId { get; private set; }
    public Funding Funding { get; private set; }
    public int TransactionId { get; private set; }
    public GoalTransaction Transaction { get; private set; }
    public string State { get; private set; }
    public string Type { get; private set; }
    public int GoalId { get; private set; }
    public Goal Goal { get; private set; }
    public User OwnerId { get; private set; }
    public User Owner { get; private set; }
    public decimal Cost { get; private set; }
}
