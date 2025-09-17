using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskVitor.Data;
using TaskVitor.Models;

namespace TaskVitor.Controllers
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
            var totalTarefas = await _context.Tarefas.CountAsync();

            var tarefasPorClassificacao = await _context.Tarefas
                .GroupBy(t => t.Classificacao.Nome)
                .Select(g => new { Nome = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Nome, x => x.Count);

            var tarefasPorCliente = await _context.Tarefas
                .GroupBy(t => t.Cliente.Nome)
                .Select(g => new { Nome = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Nome, x => x.Count);

            var tarefasPorDiaSemanaQuery = await _context.Tarefas
                .GroupBy(t => (int)t.Data.DayOfWeek) // Sunday = 0 ... Saturday = 6
                .Select(g => new { Dia = g.Key, Count = g.Count() })
                .ToListAsync();

            var tarefasPorDiaSemana = new int[7];
            foreach (var item in tarefasPorDiaSemanaQuery)
            {
                tarefasPorDiaSemana[item.Dia] = item.Count;
            }

            var viewModel = new Dashboard
            {
                TotalTarefas = totalTarefas,
                TarefasPorClassificacao = tarefasPorClassificacao,
                TarefasPorCliente = tarefasPorCliente,
                TarefasPorDiaSemana = tarefasPorDiaSemana
            };

            return View(viewModel);
        }
    }
}
