using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskVitor.Data;
using TaskVitor.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskVitor.Controllers
{
    public class LembretesController : Controller
    {
        private readonly AppDbContext _context;

        public LembretesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Lembrete
        public async Task<IActionResult> Index(string filtro = "naoConcluidos")
        {
            var query = _context.Lembretes.AsQueryable();

            switch (filtro)
            {
                case "todos":
                    // mostra todos
                    break;
                case "concluidos":
                    query = query.Where(l => l.Concluido);
                    break;
                case "atrasados":
                    query = query.Where(l => !l.Concluido && l.DataHora < DateTime.Now);
                    break;
                default:
                    // naoConcluidos (padrão)
                    query = query.Where(l => !l.Concluido);
                    break;
            }

            var lembretes = await query
                .OrderBy(l => l.DataHora)
                .ToListAsync();

            return View(lembretes.ToList());
        }

        // GET: Lembrete/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lembrete/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lembrete lembrete)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lembrete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lembrete);
        }

        // GET: Lembrete/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var lembrete = await _context.Lembretes.FindAsync(id);
            if (lembrete == null)
                return NotFound();

            return View(lembrete);
        }

        // POST: Lembrete/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Lembrete lembrete)
        {
            if (id != lembrete.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lembrete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LembreteExists(lembrete.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lembrete);
        }

        // GET: Lembrete/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var lembrete = await _context.Lembretes.FindAsync(id);
            if (lembrete == null)
                return NotFound();

            _context.Lembretes.Remove(lembrete);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Lembrete/Concluir/5
        public async Task<IActionResult> Concluir(int id)
        {
            var lembrete = await _context.Lembretes.FindAsync(id);
            if (lembrete == null)
                return NotFound();

            lembrete.Concluido = true;
            _context.Update(lembrete);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool LembreteExists(int id)
        {
            return _context.Lembretes.Any(e => e.Id == id);
        }
    }
}
