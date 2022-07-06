using client.application.Contracts.Persistence;
using client.core;
using client.infrastructure.Data;

namespace client.infrastructure.Repositories
{
    public class PersonRepository: RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(BankDbContext dbContext) : base(dbContext)
        {
        }
    }
}