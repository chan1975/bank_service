using System.Linq;
using System.Threading.Tasks;
using client.application.Contracts.Persistence;
using client.core;
using client.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace client.infrastructure.Repositories
{
    public class ClientRepository: RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(BankDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Client> GetClientByPersonId(int personId)
        {
            return await _context.Clients.Where(c => c.PersonId == personId).FirstOrDefaultAsync();
        }
    }
  
}