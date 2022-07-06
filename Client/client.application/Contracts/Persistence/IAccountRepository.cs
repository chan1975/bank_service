using System.Threading.Tasks;
using client.core;

namespace client.application.Contracts.Persistence
{
    public interface IAccountRepository: IAsyncRepository<Account>
    {
       Task<Account> GetByNumber(int number);
    }
}