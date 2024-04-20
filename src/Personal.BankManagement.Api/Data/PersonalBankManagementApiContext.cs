using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Personal.BankManagement.Domain;

namespace Personal.BankManagement.Api.Data
{
    public class PersonalBankManagementApiContext : DbContext
    {
        public PersonalBankManagementApiContext (DbContextOptions<PersonalBankManagementApiContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Personal.BankManagement.Domain.Bank> Bank { get; set; } = default!;
        public DbSet<Personal.BankManagement.Domain.BankAccount> BankAccount { get; set; } = default!;
        public DbSet<Personal.BankManagement.Domain.Person> Person { get; set; } = default!;
        public DbSet<Personal.BankManagement.Domain.TransactionalHistory> TransactionalHistory { get; set; } = default!;
        public DbSet<Personal.BankManagement.Domain.User> User { get; set; } = default!;
    }
}
