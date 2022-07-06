using client.core;
using client.infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace client.infrastructure.Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
        {
        }
        
        public DbSet<Client> Clients { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new PersonEntityConfiguration().Configure(modelBuilder.Entity<Person>());
            new ClientEntityConfiguration().Configure(modelBuilder.Entity<Client>());
            new AccountEntityConfiguration().Configure(modelBuilder.Entity<Account>());
            new TransactionEntityConfiguration().Configure(modelBuilder.Entity<Transaction>());
            
        }
    }
}