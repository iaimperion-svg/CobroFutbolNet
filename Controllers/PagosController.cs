using CobroFutbol.Web.Data;
using CobroFutbol.Web.Models;
using CobroFutbol.Web.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CobroFutbol.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PagosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("recibir")]
        public async Task<IActionResult> Recibir([FromBody] RecibirPagoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hashNormalizado = string.IsNullOrWhiteSpace(request.HashComprobante)
                ? null
                : request.HashComprobante.Trim();

            if (!string.IsNullOrWhiteSpace(hashNormalizado))
            {
                var pagoExistente = await _context.Pagos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.HashComprobante == hashNormalizado);

                if (pagoExistente != null)
                {
                    return Conflict(new
                    {
                        ok = false,
                        mensaje = "Ya existe un pago con ese comprobante.",
                        idExistente = pagoExistente.Id
                    });
                }
            }

            var pago = new Pago
            {
                Pagador = request.Pagador.Trim(),
                Alumno = string.IsNullOrWhiteSpace(request.Alumno) ? null : request.Alumno.Trim(),
                Monto = request.Monto,
                FechaPago = request.FechaPago ?? DateTime.Now,
                Estado = "Pendiente",
                ComprobanteUrl = string.IsNullOrWhiteSpace(request.ComprobanteUrl) ? null : request.ComprobanteUrl.Trim(),
                Notas = string.IsNullOrWhiteSpace(request.Notas) ? null : request.Notas.Trim(),
                FechaCreacion = DateTime.Now,

                PagadorId = request.PagadorId,
                AlumnoId = request.AlumnoId,
                MontoReportado = request.MontoReportado,
                FechaPagoReportada = request.FechaPagoReportada,
                CanalIngreso = string.IsNullOrWhiteSpace(request.CanalIngreso) ? null : request.CanalIngreso.Trim(),
                OrigenMensajeId = string.IsNullOrWhiteSpace(request.OrigenMensajeId) ? null : request.OrigenMensajeId.Trim(),
                HashComprobante = hashNormalizado,
                TextoExtraido = string.IsNullOrWhiteSpace(request.TextoExtraido) ? null : request.TextoExtraido.Trim(),
                ConfianzaIA = request.ConfianzaIA,
                EstadoConciliacion = string.IsNullOrWhiteSpace(request.EstadoConciliacion) ? null : request.EstadoConciliacion.Trim(),
                FechaRevision = request.FechaRevision,
                RevisadoPor = string.IsNullOrWhiteSpace(request.RevisadoPor) ? null : request.RevisadoPor.Trim()
            };

            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                ok = true,
                mensaje = "Pago recibido correctamente",
                id = pago.Id,
                estado = pago.Estado,
                estadoConciliacion = pago.EstadoConciliacion
            });
        }
    }
}