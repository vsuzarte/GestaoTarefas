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

        //TODO: Fazer refatoração para dividir esse metodo GIGANTE!
        public async Task<IActionResult> Index(DateTime? dataInicio, DateTime? dataFim)
        {
            if (!dataInicio.HasValue || !dataFim.HasValue)
            {
                var hoje = DateTime.Today;

                // Calcula terça-feira da semana passada (ou desta semana se hoje >= terça)
                int diffTercaPassada = ((int)hoje.DayOfWeek - (int)DayOfWeek.Tuesday + 7) % 7;
                dataInicio = hoje.AddDays(-diffTercaPassada);

                // Próxima terça = dataInicio + 7 dias
                dataFim = dataInicio.Value.AddDays(7);
            }

            // Filtra tarefas dentro do range
            var tarefasQuery = _context.Tarefas
                .Where(t => t.Data.Date >= dataInicio.Value.Date && t.Data.Date <= dataFim.Value.Date);

            // Total de tarefas
            var totalTarefas = await tarefasQuery.CountAsync();

            // Tarefas por classificação
            var tarefasPorClassificacao = await tarefasQuery
                .GroupBy(t => t.Classificacao.Nome)
                .Select(g => new { Nome = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Nome, x => x.Count);

            // Tarefas por cliente
            var tarefasPorCliente = await tarefasQuery
                .GroupBy(t => t.Cliente.Nome)
                .Select(g => new { Nome = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Nome, x => x.Count);

            // Tarefas por projeto
            var tarefasPorProjeto = await tarefasQuery
                .GroupBy(t => t.Projeto.Nome)
                .Select(g => new { Nome = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Nome, x => x.Count);

            // Tarefas por dia da semana
            var tarefasPorDiaSemanaQuery = await tarefasQuery
                .GroupBy(t => (int)t.Data.DayOfWeek)
                .Select(g => new { Dia = g.Key, Count = g.Count() })
                .ToListAsync();

            var tarefasPorDiaSemana = Enumerable.Range(0, 7)
                .Select(dia => tarefasPorDiaSemanaQuery.FirstOrDefault(x => x.Dia == dia)?.Count ?? 0)
                .ToArray();

            // Projeto com mais tarefas
            var projetoComMaisTarefas = await tarefasQuery
                .GroupBy(t => t.Projeto)
                .Select(g => new
                {
                    Projeto = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Count)
                .FirstOrDefaultAsync();

            // Dia mais produtivo
            var diaMaisProdutivoQuery = tarefasPorDiaSemanaQuery
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            string diaMaisProdutivo = diaMaisProdutivoQuery switch
            {
                null => "Nenhum",
                var x => x.Dia switch
                {
                    0 => "Domingo",
                    1 => "Segunda-feira",
                    2 => "Terça-feira",
                    3 => "Quarta-feira",
                    4 => "Quinta-feira",
                    5 => "Sexta-feira",
                    6 => "Sábado",
                    _ => "Desconhecido"
                }
            };

            int quantidadeTarefasDiaMaisProdutivo = diaMaisProdutivoQuery?.Count ?? 0;

            var tempoPorProjeto = _context.Tarefas
                                    .Include(t => t.Apontamentos)
                                    .Include(t => t.Projeto)
                                    .Where(t => t.Data >= dataInicio && t.Data <= dataFim)
                                    .GroupBy(t => t.Projeto!.Nome)
                                    .ToDictionary(
                                        g => g.Key!,
                                        g => g.Sum(t => t.Apontamentos.Sum(a =>
                                            a.DuracaoManual?.TotalSeconds
                                            ?? (a.Fim.HasValue ? (a.Fim.Value - a.Inicio).TotalSeconds
                                            : (DateTime.Now - a.Inicio).TotalSeconds)))
                                    );

            var tempoPorClassificacao = _context.Tarefas
                                   .Include(t => t.Apontamentos)
                                   .Include(t => t.Classificacao)
                                   .Where(t => t.Data >= dataInicio && t.Data <= dataFim)
                                   .GroupBy(t => t.Classificacao!.Nome)
                                   .ToDictionary(
                                       g => g.Key!,
                                       g => g.Sum(t => t.Apontamentos.Sum(a =>
                                           a.DuracaoManual?.TotalSeconds
                                           ?? (a.Fim.HasValue ? (a.Fim.Value - a.Inicio).TotalSeconds
                                           : (DateTime.Now - a.Inicio).TotalSeconds)))
                                   );

            var viewModel = new Dashboard
            {
                TotalTarefas = totalTarefas,
                TarefasPorClassificacao = tarefasPorClassificacao,
                TarefasPorCliente = tarefasPorCliente,
                TarefasPorDiaSemana = tarefasPorDiaSemana,
                TarefasPorProjeto = tarefasPorProjeto,
                ProjetoComMaisTarefas = projetoComMaisTarefas?.Projeto?.Nome,
                QuantidadeTarefasProjeto = projetoComMaisTarefas?.Count ?? 0,
                DiaMaisProdutivo = diaMaisProdutivo,
                QuantidadeTarefasDiaMaisProdutivo = quantidadeTarefasDiaMaisProdutivo,
                DataInicio = dataInicio.Value,
                DataFim = dataFim.Value,
                TempoPorProjeto = tempoPorProjeto,
                TempoPorClassificacao = tempoPorClassificacao
            };

            return View(viewModel);
        }
    }
}
