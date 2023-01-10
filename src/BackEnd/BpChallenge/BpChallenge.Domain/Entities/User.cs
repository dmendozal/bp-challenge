using System.ComponentModel.DataAnnotations.Schema;

namespace BpChallenge.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; private set; }
    public string Surname { get; private set; }
    public int AdvisorId { get; private set; }
    public User Advisor { get; private set; }
    public int CurrencyId { get; private set; }
    public Currency Currency { get; private set; }

}
