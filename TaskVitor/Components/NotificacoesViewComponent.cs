using Microsoft.AspNetCore.Mvc;
using TaskVitor.Data;

public class NotificacoesViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public NotificacoesViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var hoje = DateTime.Now.Date;

        var lembretes = _context.Lembretes
            .Where(l => !l.Concluido)
            .Where(l => l.DataHora.Date <= hoje.AddDays(l.DiasAntecedencia))
            .OrderBy(l => l.DataHora)
            .ToList();

        return View(lembretes);
    }
}
