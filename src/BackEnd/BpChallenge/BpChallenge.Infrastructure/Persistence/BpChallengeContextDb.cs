using BpChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BpChallenge.Infrastructure.Persistence;

public class BpChallengeContextDb : DbContext
{
    public BpChallengeContextDb(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> User => Set<User>();
    public DbSet<CompositionCategory> CompositionCategory => Set<CompositionCategory>();
    public DbSet<CompositionSubCategory> CompositionSubCategory => Set<CompositionSubCategory>();
    public DbSet<Currency> Currency => Set<Currency>();
    public DbSet<CurrencyIndicator> CurrencyIndicator => Set<CurrencyIndicator>();
    public DbSet<Funding> Funding => Set<Funding>();
    public DbSet<FundingComposition> FundingComposition => Set<FundingComposition>();
    public DbSet<FundingShareValue> FundingShareValue => Set<FundingShareValue>();
    public DbSet<GoalTransaction> GoalTransaction => Set<GoalTransaction>();
    public DbSet<GoalTransactionFunding> GoalTransactionFunding => Set<GoalTransactionFunding>();
    public DbSet<Goal> Goal => Set<Goal>();
    public DbSet<GoalCategory> GoalCategory => Set<GoalCategory>();
    public DbSet<FinancialEntity> FinancialEntity => Set<FinancialEntity>();
    public DbSet<InvestmentStrategy> InvestmentStrategy => Set<InvestmentStrategy>();
    public DbSet<InvestmentStrategyType> InvestmentStrategyType => Set<InvestmentStrategyType>();
    public DbSet<Portafolio> Portafolio => Set<Portafolio>();
    public DbSet<PortafolioComposition> PortafolioComposition => Set<PortafolioComposition>();
    public DbSet<PortafolioFunding> PortafolioFunding => Set<PortafolioFunding>();
    public DbSet<RiskLevel> RiskLevel => Set<RiskLevel>();
}
