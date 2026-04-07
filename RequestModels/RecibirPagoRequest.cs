using System;
using System.ComponentModel.DataAnnotations;

namespace CobroFutbol.Web.RequestModels
{
    public class RecibirPagoRequest
    {
        [Required]
        public string Pagador { get; set; } = string.Empty;

        public string? Alumno { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal Monto { get; set; }

        public DateTime? FechaPago { get; set; }

        public string? ComprobanteUrl { get; set; }

        public string? Notas { get; set; }

        public int? PagadorId { get; set; }

        public int? AlumnoId { get; set; }

        public decimal? MontoReportado { get; set; }

        public DateTime? FechaPagoReportada { get; set; }

        public string? CanalIngreso { get; set; }

        public string? OrigenMensajeId { get; set; }

        public string? HashComprobante { get; set; }

        public string? TextoExtraido { get; set; }

        [Range(0, 100, ErrorMessage = "La confianza IA debe estar entre 0 y 100.")]
        public decimal? ConfianzaIA { get; set; }

        public string? EstadoConciliacion { get; set; }

        public DateTime? FechaRevision { get; set; }

        public string? RevisadoPor { get; set; }
    }
}