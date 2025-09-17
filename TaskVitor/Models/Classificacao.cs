namespace TaskVitor.Models
{
    public class Classificacao
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public ICollection<Tarefa> Tarefas { get; set; }
    }
}
