using client.application.Contracts.Persistence;
using client.core;
using client.infrastructure.Data;

namespace client.infrastructure.Repositories
{
    public class TransactionRepository: RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BankDbContext dbContext) : base(dbContext)
        {
        }
    }
}