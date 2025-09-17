using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskVitor.Data;
using TaskVitor.Models;

namespace TaskVitor.Controllers
{
    public class ResponsaveisController : Controller
    {
        private readonly AppDbContext _context;

        public ResponsaveisController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Responsaveis.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Responsavel responsavel)
        {
            if (ModelState.IsValid)
            {
                _context.Responsaveis.Add(responsavel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(responsavel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var responsavel = await _context.Responsaveis.FindAsync(id);
            if (responsavel == null)
                return NotFound();

            return View(responsavel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Responsavel responsavel)
        {
            if (id != responsavel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(responsavel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Responsaveis.Any(e => e.Id == responsavel.Id))
                        return NotFound();
                    else
                        throw;
                }
            }
            return View(responsavel);
        }

    }
}