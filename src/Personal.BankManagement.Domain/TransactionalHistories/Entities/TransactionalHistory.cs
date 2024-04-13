namespace Personal.BankManagement.Domain;

public class TransactionalHistory
{
    public Guid Id { get; set; }
    public Guid BankAccountId { get; set; }
    public int Value{get;set;}
    public TransactionType TransactionType{get;set;}
    public DateTime TransactionDate{get;set;}
    public string? Description{get;set;}
    public string Currency{get;set;}
    public bool IsExecuted{get;set;}
    public virtual BankAccount BankAccount{get;set;}
    
}

public enum TransactionType : byte
{
    Deposit,
    Withdraw
}
