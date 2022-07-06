using System.Collections.Generic;
using System.Threading.Tasks;

namespace client.application.Features.Client
{
    public interface IClientService
    {
        Task<core.Client> GetClient(int id);
        Task<IEnumerable<core.Client>> GetAllClients();
        Task<core.Client> CreateClient(core.Client client);
        Task<core.Client> UpdateClient(core.Client client);
        Task<core.Client> DeleteClient(int id);

    }
}