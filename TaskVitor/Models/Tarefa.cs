using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskVitor.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public int ClassificacaoId { get; set; }

        public int ClienteId { get; set; }

        public int ResponsavelId { get; set; }

        public int ProjetoId { get; set; }
        
        public Cliente Cliente { get; set; }

        public Classificacao Classificacao { get; set; }

        public Responsavel Responsavel { get; set; }

        public Projeto Projeto { get; set; }

        public ICollection<Apontamento> Apontamentos { get; set; } = new List<Apontamento>();

        [NotMapped]
        public TimeSpan TempoTotal => TimeSpan.FromTicks(
            Apontamentos
                .Where(a => a.Duracao.HasValue)
                .Sum(a => a.Duracao!.Value.Ticks)
        );

        [NotMapped]
        public bool EmAndamento { get; set; }
    }
}
