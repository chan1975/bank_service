using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using client.application.Contracts.Persistence;
using client.application.Execptions;

namespace client.application.Features.Reports
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDtoOut>> GetReportByDates(int clientId, DateTime startDate, DateTime endDate);
    }
    
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReportDtoOut>> GetReportByDates(int clientId, DateTime startDate, DateTime endDate)
        {
            var client = await _unitOfWork.ClientRepository.GetByIdAsync(clientId);
            if(client is null) throw  new BadRequestExeption("No existe el cliente con el id: " + clientId);
            if(startDate > endDate) throw new BadRequestExeption("La fecha de inicio no puede ser mayor a la fecha de fin");
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(client.PersonId);
            var accounts = await _unitOfWork.AccountRepository.GetByUserId(clientId);
            var listReport = new List<ReportDtoOut>();
            foreach (var account in accounts)
            {
                var tranasactions = await _unitOfWork.TransactionRepository.GetTransactionsByInterval(account.Id, startDate, endDate);
                foreach (var transaction in tranasactions)
                {
                    var report = new ReportDtoOut
                    {
                        Cliente = person.Name,
                        Estado = account.IsActive,
                        Fecha = transaction.Date,
                        Movimiento = transaction.Amount,
                        Tipo = account.Type,
                        NumeroCuenta = account.Number.ToString(),
                        SaldoDisponible = transaction.Balance,
                        SaldoInicial = account.Balance
                    };
                    listReport.Add(report);
                }
            }
            return listReport;
        }
    }
}