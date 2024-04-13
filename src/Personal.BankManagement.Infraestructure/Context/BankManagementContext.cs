using Personal.BankManagement.Domain;
using Microsoft.EntityFrameworkCore;


namespace Personal.BankManagement.Infraestructure;

public class  BankManagementContext(DbContextOptions<BankManagementContext> options) : DbContext(options)
{

    public DbSet<BankAccount> BankAccounts { get; set; }

    public DbSet<TransactionalHistory> TransactionalHistories { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Person> Persons { get; set; }

    public DbSet<Bank> Banks { get; set; }
}
