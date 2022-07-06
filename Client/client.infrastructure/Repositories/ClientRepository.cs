using client.application.Contracts.Persistence;
using client.core;
using client.infrastructure.Data;

namespace client.infrastructure.Repositories
{
    public class ClientRepository: RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(BankDbContext dbContext) : base(dbContext)
        {
        }
    }
  
}