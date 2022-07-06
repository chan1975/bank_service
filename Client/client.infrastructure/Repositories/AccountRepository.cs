using System.Linq;
using System.Threading.Tasks;
using client.application.Contracts.Persistence;
using client.core;
using client.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace client.infrastructure.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(BankDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Account> GetByNumber(int number)
        {
            return await _context.Accounts.Where(x => x.Number == number).FirstOrDefaultAsync();
        }
    }
}