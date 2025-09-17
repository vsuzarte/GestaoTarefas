using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskVitor.Data;
using TaskVitor.Models;

namespace TaskVitor.Controllers
{
    public class TarefasController : Controller
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? dataInicio, DateTime? dataFim)
        {
            // Definindo range da sprint: terça passada até terça atual
            var hoje = DateTime.Today;
            var diasDesdeTerça = ((int)hoje.DayOfWeek + 5) % 7; // terça = 2
            var ultimaTerca = hoje.AddDays(-diasDesdeTerça);
            var proximaTerca = ultimaTerca.AddDays(7);

            var inicio = dataInicio ?? ultimaTerca;
            var fim = dataFim ?? proximaTerca;

            var tarefas = await _context.Tarefas
                .Include(t => t.Classificacao)
                .Include(t => t.Cliente)
                .Include(t => t.Responsavel)
                .Include(t => t.Projeto)
                .Where(t => t.Data >= inicio && t.Data <= fim)
                .OrderBy(t => t.Data)
                .ToListAsync();

            ViewBag.DataInicio = inicio.ToString("yyyy-MM-dd");
            ViewBag.DataFim = fim.ToString("yyyy-MM-dd");

            return View(tarefas);
        }


        // Criar tarefa
        public IActionResult Create()
        {
            ViewBag.Classificacoes = _context.Classificacoes.ToList();
            ViewBag.Clientes = _context.Clientes.ToList();
            ViewBag.Responsaveis = _context.Responsaveis.ToList();
            ViewBag.Projetos = _context.Projetos.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _context.Tarefas.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var tarefa = await _context.Tarefas
                .Include(t => t.Classificacao)
                .Include(t => t.Cliente)
                .Include(t => t.Responsavel)
                .Include(t => t.Projeto)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarefa == null)
                return NotFound();

            ViewBag.Classificacoes = _context.Classificacoes.ToList();
            ViewBag.Clientes = _context.Clientes.ToList();
            ViewBag.Responsaveis = _context.Responsaveis.ToList();
            ViewBag.Projetos = _context.Projetos.ToList();

            return View(tarefa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tarefa tarefa)
        {
            if (id != tarefa.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tarefas.Any(e => e.Id == tarefa.Id))
                        return NotFound();
                    else
                        throw;
                }
            }
            return View(tarefa);
        }
    }
}