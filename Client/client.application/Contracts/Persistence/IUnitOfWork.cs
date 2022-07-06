using System;
using System.Threading.Tasks;
using client.core.Common;

namespace client.application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository PersonRepository { get; }
        IClientRepository ClientRepository { get; }
        IAccountRepository AccountRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel;

        Task<int> Complete();
    }
}