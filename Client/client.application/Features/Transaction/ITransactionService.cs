using System.Threading.Tasks;

namespace client.application.Features.Transaction
{
    public interface ITransactionService
    {
        Task<core.Transaction> CreateTransaction(core.Transaction transaction);
        Task<core.Transaction> DeleteTransaction(int id);
    }
}