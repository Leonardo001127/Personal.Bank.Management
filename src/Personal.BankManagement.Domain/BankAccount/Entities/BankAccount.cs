namespace Personal.BankManagement.Domain;

public class BankAccount
{
    public Guid Id{get;set;}
    public virtual Bank Bank{get;set;}
    public virtual Person Person{get;set;}
    public string? BankAccountGoal { get; set; }
}
