using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskVitor.Data;
using TaskVitor.Models;

namespace TaskVitor.Controllers
{
    public class ProjetosController : Controller
    {
        private readonly AppDbContext _context;

        public ProjetosController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Projetos.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                _context.Projetos.Add(projeto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projeto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto == null)
                return NotFound();

            return View(projeto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Projeto projeto)
        {
            if (id != projeto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projeto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Projetos.Any(e => e.Id == projeto.Id))
                        return NotFound();
                    else
                        throw;
                }
            }
            return View(projeto);
        }

    }
}