using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using client.application.Contracts.Persistence;
using client.core;
using client.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace client.infrastructure.Repositories
{
    public class TransactionRepository: RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BankDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> GetCurrentBalance(int accountId)
        {
            var balance = await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Id)
                .FirstOrDefaultAsync();
            return balance.Balance;
        }

        public async Task<bool> ExistsTransactionByAccountId(int accountId)
        {
            var exists = await _context.Transactions.AnyAsync(t => t.AccountId == accountId);
            return exists;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByInterval(int accountId, DateTime dateInit, DateTime dateEnd)
        {
            var transactions = await _context.Transactions.Where(t => t.AccountId == accountId && t.Date >= dateInit && t.Date <= dateEnd).ToListAsync();
            return transactions;
        }
    }
}