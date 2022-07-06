using client.application.Contracts.Persistence;
using client.core;
using client.infrastructure.Data;

namespace client.infrastructure.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(BankDbContext dbContext) : base(dbContext)
        {
        }
    }
}