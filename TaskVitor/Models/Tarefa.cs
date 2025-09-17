using System.ComponentModel.DataAnnotations;

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
    }
}
