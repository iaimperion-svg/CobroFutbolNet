using CobroFutbol.Web.Data;
using CobroFutbol.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CobroFutbol.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Dashboard";

            var inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var finMes = inicioMes.AddMonths(1);

            var totalRecaudadoMes = await _context.Pagos
                .Where(p => p.Estado == "Aprobado"
                    && p.FechaPago >= inicioMes
                    && p.FechaPago < finMes)
                .SumAsync(p => (decimal?)p.Monto) ?? 0;

            var viewModel = new DashboardViewModel
            {
                CantidadPendientes = await _context.Pagos.CountAsync(p => p.Estado == "Pendiente"),
                CantidadValidados = await _context.Pagos.CountAsync(p => p.Estado == "Aprobado"),
                CantidadRechazados = await _context.Pagos.CountAsync(p => p.Estado == "Rechazado"),

                TotalRecaudadoMes = totalRecaudadoMes,

                PagosPendientes = await _context.Pagos
                    .Where(p => p.Estado == "Pendiente")
                    .OrderByDescending(p => p.FechaPago)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Validar(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);

            if (pago == null)
                return NotFound();

            pago.Estado = "Aprobado";

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Rechazar(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);

            if (pago == null)
                return NotFound();

            pago.Estado = "Rechazado";

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RecaudacionMensual()
        {
            var inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var finMes = inicioMes.AddMonths(1);

            var pagos = await _context.Pagos
                .Where(p => p.Estado == "Aprobado"
                    && p.FechaPago >= inicioMes
                    && p.FechaPago < finMes)
                .OrderByDescending(p => p.FechaPago)
                .ToListAsync();

            return View(pagos);
        }
    }
}