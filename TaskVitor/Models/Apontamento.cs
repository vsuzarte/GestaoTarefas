using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskVitor.Models
{
    public class Apontamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TarefaId { get; set; }

        [ForeignKey(nameof(TarefaId))]
        public Tarefa Tarefa { get; set; } = default!;

        [Required]
        public DateTime Inicio { get; set; }

        public DateTime? Fim { get; set; }

        // Se o usuário inserir manualmente a duração (sem Fim calculado)
        public TimeSpan? DuracaoManual { get; set; }

        [NotMapped]
        public TimeSpan? Duracao =>
            DuracaoManual ?? (Fim.HasValue ? Fim.Value - Inicio : null);
    }
}
