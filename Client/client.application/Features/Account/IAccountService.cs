using System.Collections.Generic;
using System.Threading.Tasks;

namespace client.application.Features.Account
{
    public interface IAccountService
    {
        Task<core.Account> GetAccountAsync(int accountId);
        Task<IEnumerable<core.Account>> GetAllAccountsAsync();
        Task<core.Account> CreateAccountAsync(core.Account account);
        Task<core.Account> UpdateAccountAsync(core.Account account);
        Task<core.Account> DeleteAccountAsync(int accountId);
    }
}