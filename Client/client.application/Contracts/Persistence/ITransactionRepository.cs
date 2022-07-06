using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using client.core;

namespace client.application.Contracts.Persistence
{
    public interface ITransactionRepository: IAsyncRepository <Transaction>
    {
        Task<int> GetCurrentBalance(int accountId);
        Task<bool> ExistsTransactionByAccountId(int accountId);
        Task<IEnumerable<Transaction>> GetTransactionsByInterval(int accountId, DateTime dateInit, DateTime dateEnd);
    }
}