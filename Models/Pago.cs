using System;

namespace CobroFutbol.Web.Models
{
    public class Pago
    {
        public int Id { get; set; }

        public string Pagador { get; set; } = string.Empty;

        public string? Alumno { get; set; }

        public decimal Monto { get; set; }

        public DateTime FechaPago { get; set; }

        public string Estado { get; set; } = "Pendiente";
        // Pendiente | Aprobado | Rechazado | Observado | Conciliado | Aplicado

        public string? ComprobanteUrl { get; set; }

        public string? Notas { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public int? PagadorId { get; set; }

        public int? AlumnoId { get; set; }

        public decimal? MontoReportado { get; set; }

        public DateTime? FechaPagoReportada { get; set; }

        public string? CanalIngreso { get; set; }

        public string? OrigenMensajeId { get; set; }

        public string? HashComprobante { get; set; }

        public string? TextoExtraido { get; set; }

        public decimal? ConfianzaIA { get; set; }

        public string? EstadoConciliacion { get; set; }

        public DateTime? FechaRevision { get; set; }

        public string? RevisadoPor { get; set; }
    }
}