using CobroFutbol.Web.Models;

namespace CobroFutbol.Web.ViewModels
{
    public class DashboardViewModel
    {
        public int CantidadPendientes { get; set; }

        public int CantidadValidados { get; set; }

        public int CantidadRechazados { get; set; }

        public decimal TotalRecaudadoMes { get; set; }

        public List<Pago> PagosPendientes { get; set; } = new();
    }
}