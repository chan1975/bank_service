using System.Collections.Generic;
using System.Threading.Tasks;
using client.application.Contracts.Persistence;
using client.application.Execptions;

namespace client.application.Features.Account
{
    public class AccountService: IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<core.Account> GetAccountAsync(int accountId)
        {
            var accunt = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            if(accunt is null) throw new NotFoundExepction(nameof(core.Account), accountId);
            return accunt;
        }

        public async Task<IEnumerable<core.Account>> GetAllAccountsAsync()
        {
            var accounts = await _unitOfWork.AccountRepository.GetAllAsync();
            return accounts;
        }

        public async Task<core.Account> CreateAccountAsync(core.Account account)
        {
            var client = await _unitOfWork.ClientRepository.GetByIdAsync(account.ClientId);
            if(client is null) throw new BadRequestExeption("No se puede crear una cuenta sin cliente");
            var accountToCreate = await _unitOfWork.AccountRepository.GetByNumber(account.Number);
            if ( accountToCreate != null) throw new BadRequestExeption("Ya existe una cuenta con el numero: " + account.Number);
            var newAccount = await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.Complete();
            return newAccount;
        }

        public async Task<core.Account> UpdateAccountAsync(core.Account account)
        {
            var client = await _unitOfWork.ClientRepository.GetByIdAsync(account.ClientId);
            if(client is null) throw new BadRequestExeption("No se puede actualizar una cuenta sin cliente");
            var accountToUpdate = await _unitOfWork.AccountRepository.GetByIdAsync(account.Id);
            if(accountToUpdate is null) throw new NotFoundExepction(nameof(core.Account), account.Id);
            accountToUpdate.Balance = account.Balance;
            accountToUpdate.ClientId = account.ClientId;
            accountToUpdate.Number = account.Number;
            accountToUpdate.Type = account.Type;
            accountToUpdate.IsActive = account.IsActive;
            await _unitOfWork.Complete();
            return accountToUpdate;
        }

        public async Task<core.Account> DeleteAccountAsync(int accountId)
        {
            var accountToDelete = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            if(accountToDelete is null) throw new NotFoundExepction(nameof(core.Account), accountId);
            await _unitOfWork.AccountRepository.DeleteAsync(accountToDelete);
            await _unitOfWork.Complete();
            return accountToDelete;
        }
    }
}