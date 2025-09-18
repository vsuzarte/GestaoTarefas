using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskVitor.Data;
using TaskVitor.Models;

namespace TaskVitor.Controllers
{
    public class ApontamentosController : Controller
    {
        private readonly AppDbContext _context;

        public ApontamentosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Alternar(int id)
        {
            var tarefa = await _context.Tarefas
                .Include(t => t.Apontamentos)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarefa == null)
                return NotFound();

            var apontamentoAberto = tarefa.Apontamentos.FirstOrDefault(a => a.Fim == null);

            if (apontamentoAberto == null)
            {
                // Iniciar apontamento
                var novoApontamento = new Apontamento
                {
                    TarefaId = tarefa.Id,
                    Inicio = DateTime.Now
                };

                _context.Apontamentos.Add(novoApontamento);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Parar apontamento
                apontamentoAberto.Fim = DateTime.Now;
                _context.Apontamentos.Update(apontamentoAberto);
                await _context.SaveChangesAsync();
            }

            // Calcula o tempo total da tarefa
            var tempoTotalSegundos = tarefa.Apontamentos
                .Sum(a =>
                {
                    if (a.DuracaoManual.HasValue) return a.DuracaoManual.Value.TotalSeconds;
                    if (a.Fim.HasValue) return (a.Fim.Value - a.Inicio).TotalSeconds;
                    return (DateTime.Now - a.Inicio).TotalSeconds;
                });

            return Json(new
            {
                emAndamento = apontamentoAberto == null,
                tempoTotalSegundos
            });
        }
    }
}