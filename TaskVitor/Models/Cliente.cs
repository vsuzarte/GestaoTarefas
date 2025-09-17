using System.ComponentModel.DataAnnotations;

namespace TaskVitor.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        public ICollection<Tarefa> Tarefas { get; set; }
    }
}
