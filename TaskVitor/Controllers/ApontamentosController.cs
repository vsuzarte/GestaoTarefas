using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskVitor.Data;
using TaskVitor.DTOs;
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

            bool iniciando = false;

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
                iniciando = true;
            }
            else
            {
                // Parar apontamento
                apontamentoAberto.Fim = DateTime.Now;
                _context.Apontamentos.Update(apontamentoAberto);
                await _context.SaveChangesAsync();
            }

            // Calcula o tempo total da tarefa considerando DuracaoManual
            var tempoTotalSegundos = tarefa.Apontamentos.Sum(a =>
                a.DuracaoManual?.TotalSeconds
                ?? (a.Fim.HasValue ? (a.Fim.Value - a.Inicio).TotalSeconds
                : (DateTime.Now - a.Inicio).TotalSeconds)
            );

            return Json(new
            {
                emAndamento = iniciando,
                tempoTotalSegundos
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar([FromBody] ApontamentoDTO model)
        {
            // Verifica se Inicio é válido
            if (model.Inicio == default)
                return Json(new { sucesso = false, mensagem = "Data de início inválida." });

            // Verifica se Fim é posterior a Inicio, se preenchido
            if (model.Fim.HasValue && model.Fim <= model.Inicio)
                return Json(new { sucesso = false, mensagem = "Data de fim deve ser posterior à data de início." });

            var apontamento = await _context.Apontamentos.FindAsync(model.Id);
            if (apontamento == null)
                return Json(new { sucesso = false, mensagem = "Apontamento não encontrado." });

            apontamento.Inicio = model.Inicio;
            apontamento.Fim = model.Fim;

            _context.Apontamentos.Update(apontamento);
            await _context.SaveChangesAsync();

            // Recalcula tempo total da tarefa
            var tarefa = await _context.Tarefas
                .Include(t => t.Apontamentos)
                .FirstOrDefaultAsync(t => t.Id == apontamento.TarefaId);

            double tempoTotalSegundos = tarefa.Apontamentos.Sum(a =>
                a.DuracaoManual?.TotalSeconds ?? (a.Fim.HasValue ? (a.Fim.Value - a.Inicio).TotalSeconds : 0)
            );

            return Json(new { sucesso = true, tempoTotalSegundos });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apagar(int id)
        {
            var apontamento = await _context.Apontamentos.FindAsync(id);
            if (apontamento == null) return Json(new { sucesso = false });

            int tarefaId = apontamento.TarefaId;
            _context.Apontamentos.Remove(apontamento);
            await _context.SaveChangesAsync();

            var tarefa = await _context.Tarefas
                .Include(t => t.Apontamentos)
                .FirstOrDefaultAsync(t => t.Id == tarefaId);

            double tempoTotalSegundos = tarefa.Apontamentos.Sum(a =>
                a.DuracaoManual?.TotalSeconds ?? (a.Fim.HasValue ? (a.Fim.Value - a.Inicio).TotalSeconds : 0)
            );

            return Json(new { sucesso = true, tempoTotalSegundos });
        }
    }
}