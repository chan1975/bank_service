using System;
using System.Linq;
using System.Threading.Tasks;
using client.application.Contracts.Persistence;
using client.application.Execptions;

namespace client.application.Features.Transaction
{
    public class TransactionService: ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string DEBIT = "DEBITO";
        private readonly string CREDIT = "CREDITO";
        private readonly int LIMIT_DAILY_DEBIT = 1000;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<core.Transaction> CreateTransaction(core.Transaction transaction)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(transaction.AccountId);
            if(account is null) throw new BadRequestExeption("No existe la cuenta con el id: " + transaction.AccountId);
            if(transaction.Type.ToUpper() != DEBIT && transaction.Type.ToUpper() != CREDIT) throw new BadRequestExeption("El tipo de transacción debe ser: " + DEBIT + " o " + CREDIT);
            if(transaction.Type.ToUpper() == DEBIT && transaction.Amount > 0 ) throw new BadRequestExeption("El monto debe ser negativo cuando es un debito");
            if(transaction.Type.ToUpper() == CREDIT && transaction.Amount < 0) throw new BadRequestExeption("El monto debe ser positivo cuando es un credito");
            
            int currentBalance;
            var existsTransaction = await _unitOfWork.TransactionRepository.ExistsTransactionByAccountId(transaction.AccountId);
            if (existsTransaction)
            {
                currentBalance = await _unitOfWork.TransactionRepository.GetCurrentBalance(transaction.AccountId);
                if (transaction.Type.ToUpper() == DEBIT && transaction.Amount*-1 > currentBalance) throw new BadRequestExeption("Saldo no disponible");
            }
            else
            {
                currentBalance = account.Balance;
                if (transaction.Type.ToUpper() == DEBIT && transaction.Amount*-1 > currentBalance) throw new BadRequestExeption("Saldo no disponible");
            }
            
            if (transaction.Type.ToUpper() == DEBIT)
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday || DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
                {
                    var transactions = await _unitOfWork.TransactionRepository.GetTransactionsByInterval(transaction.AccountId, DateTime.Now.Date, DateTime.Now.Date );
                    var totalDebit = transactions.Where(x => x.Type.ToUpper() == DEBIT).Sum(x => x.Amount);
                    if(totalDebit*-1 + transaction.Amount*-1 > LIMIT_DAILY_DEBIT) throw new BadRequestExeption("Cupo diario Excedido");
                }
                
                
                transaction.Balance = currentBalance + transaction.Amount;
                transaction.Date = DateTime.Now.Date;
                
                var transactionCreated = await _unitOfWork.TransactionRepository.AddAsync(transaction);
                await _unitOfWork.Complete();
                return transactionCreated;
            }
            else
            {
                transaction.Balance = currentBalance + transaction.Amount;
                transaction.Date = DateTime.Now.Date;
                var transactionCreated = await _unitOfWork.TransactionRepository.AddAsync(transaction);
                await _unitOfWork.Complete();
                return transactionCreated;
            }
            
        }

        public async Task<core.Transaction> DeleteTransaction(int id)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(id);
            if(transaction is null) throw new BadRequestExeption("No existe la transacción con el id: " + id);
            await _unitOfWork.TransactionRepository.DeleteAsync(transaction);
            await _unitOfWork.Complete();
            return transaction;
        }
    }
}