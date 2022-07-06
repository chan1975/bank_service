using System.Collections.Generic;
using System.Threading.Tasks;
using client.application.Contracts.Persistence;
using client.application.Execptions;

namespace client.application.Features.Client
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<core.Client> GetClient(int id)
        {
            var client = await _unitOfWork.ClientRepository.GetByIdAsync(id);
            if (client is null) throw new NotFoundExepction(nameof(core.Client), id);
            return client;
        }

        public async Task<IEnumerable<core.Client>> GetAllClients()
        {
            return await _unitOfWork.ClientRepository.GetAllAsync();
        }

        public async Task<core.Client> CreateClient(core.Client client)
        {
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(client.PersonId);
            if(person is null) throw new BadRequestExeption("No se puede crear un cliente sin una persona asociada");
            var clientDuplicate = await _unitOfWork.ClientRepository.GetClientByPersonId(client.PersonId);
            if (clientDuplicate != null) throw new BadRequestExeption("Ya existe un cliente asociado a la persona");
            var clientCreated = await _unitOfWork.ClientRepository.AddAsync(client);
            await _unitOfWork.Complete();
            return clientCreated;
        }

        public async Task<core.Client> UpdateClient(core.Client client)
        {
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(client.PersonId);
            if(person is null) throw new BadRequestExeption("No se puede actualizar un cliente sin una persona asociada");
            var clientToUpdate = await _unitOfWork.ClientRepository.GetByIdAsync(client.Id);
            if (clientToUpdate is null) throw new NotFoundExepction(nameof(core.Client), client.Id);
            clientToUpdate.Password = client.Password;
            clientToUpdate.Status = client.Status;
            clientToUpdate.PersonId = client.PersonId;
            var clientUpdated = await _unitOfWork.ClientRepository.UpdateAsync(clientToUpdate);
            await _unitOfWork.Complete();
            return clientUpdated;
        }

        public async Task<core.Client> DeleteClient(int id)
        {
            var client = await _unitOfWork.ClientRepository.GetByIdAsync(id);
            if (client is null) throw new NotFoundExepction(nameof(core.Client), id);
            await _unitOfWork.ClientRepository.DeleteAsync(client);
            await _unitOfWork.Complete();
            return client;
        }
    }
}