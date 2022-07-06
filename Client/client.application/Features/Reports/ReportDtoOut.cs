using System;

namespace client.application.Features.Reports
{
    public class ReportDtoOut
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string NumeroCuenta { get; set; }
        public string Tipo { get; set; }
        public int  SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int Movimiento { get; set; }
        public int SaldoDisponible { get; set; }
    }
}