namespace BpChallenge.Domain.Entities;

public class Goal : BaseEntity
{
    public string Title { get; private set; }
    public int Years { get; private set; }
    public int InitialInvestment { get; private set; }
    public int MonthlyContribution { get; private set; }
    public int TargetAmount { get; private set; }
    public int UserId { get; private set; }
    public User User { get; private set; }
    public int GoalCategoryId { get; private set; }
    public GoalCategory GoalCategory { get; private set; }
    public int RiskLevelId { get; private set; }
    public RiskLevel RiskLevel { get; private set; }
    public int PortfolioId { get; private set; }
    public Portfolio Portfolio { get; private set; }
    public int FinancialEntityId { get; private set; }
    public FinancialEntity FinancialEntity { get; private set; }
    public int CurrencyId { get; private set; }
    public Currency Currency { get; private set; }
    public int DisplayCurrencyId { get; private set; }
    public Currency DisplayCurrency { get; private set; }
}
