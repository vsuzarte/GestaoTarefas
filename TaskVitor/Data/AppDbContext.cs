using Microsoft.EntityFrameworkCore;
using TaskVitor.Models;

namespace TaskVitor.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tarefa> Tarefas { get; set; }

        public DbSet<Classificacao> Classificacoes { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Responsavel> Responsaveis { get; set; }

        public DbSet<Projeto> Projetos { get; set; }

        public DbSet<Apontamento> Apontamentos { get; set; }
    }
}
