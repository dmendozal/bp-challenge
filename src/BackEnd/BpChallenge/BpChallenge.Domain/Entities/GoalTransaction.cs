using System;

namespace BpChallenge.Domain.Entities;

public class GoalTransaction : BaseEntity
{
    public string Type { get; private set; }
    public double Amount { get; private set; }
    public DateTime Date { get; private set; }
    public int OwnerId { get; private set; }
    public User Owner { get; private set; }
    public int GoalId { get; private set; }
    public Goal Goal { get; private set; }
    public bool IsProcessed { get; private set; }
    public bool All { get; private set; }
    public string State { get; private set; }
    public int FinancialEntityId { get; private set; }
    public FinancialEntity FinancialEntity { get; private set; }
    //public int FundingId { get; private set; }
    //public Funding Funding { get; private set; }
    public int CurrencyId { get; private set; }
    public Currency Currency { get; private set; }
    public double Cost { get; private set; }
}
