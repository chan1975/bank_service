using System.Threading.Tasks;
using client.core;

namespace client.application.Contracts.Persistence
{
    public interface IClientRepository: IAsyncRepository<Client>
    {
        Task<Client> GetClientByPersonId(int personId); 
    }
}