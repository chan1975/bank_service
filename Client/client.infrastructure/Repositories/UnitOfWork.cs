using System;
using System.Collections;
using System.Threading.Tasks;
using client.application.Contracts.Persistence;
using client.core.Common;
using client.infrastructure.Data;

namespace client.infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly BankDbContext _context;

        private IPersonRepository _personRepository;
        private IClientRepository _clientRepository;
        private IAccountRepository _accountRepository;
        private ITransactionRepository _transactionRepository;


        public IPersonRepository PersonRepository => _personRepository ??= new PersonRepository(_context);

        public IClientRepository ClientRepository => _clientRepository ??= new ClientRepository(_context);
        
        public IAccountRepository AccountRepository => _accountRepository ??= new AccountRepository(_context);
        
        public ITransactionRepository TransactionRepository => _transactionRepository ??= new TransactionRepository(_context);

        public UnitOfWork(BankDbContext context)
        {
            _context = context;
        }

        public BankDbContext BankDbContext => _context;

        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception("Err");
            }
            
        }

        
        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel
        {
            if (_repositories == null)
            { 
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }

    }
}