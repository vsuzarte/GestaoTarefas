using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskVitor.Data;
using TaskVitor.Models;

namespace TaskVitor.Controllers
{
    public class ClassificacoesController : Controller
    {
        private readonly AppDbContext _context;

        public ClassificacoesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Classificacoes.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Classificacao classificacao)
        {
            if (ModelState.IsValid)
            {
                _context.Classificacoes.Add(classificacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classificacao);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var classificacao = await _context.Classificacoes.FindAsync(id);
            if (classificacao == null)
                return NotFound();

            return View(classificacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Classificacao classificacao)
        {
            if (id != classificacao.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classificacao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Classificacoes.Any(e => e.Id == classificacao.Id))
                        return NotFound();
                    else
                        throw;
                }
            }
            return View(classificacao);
        }

    }
}