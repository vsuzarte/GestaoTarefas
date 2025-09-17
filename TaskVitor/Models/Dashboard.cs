namespace TaskVitor.Models
{
    public class Dashboard
    {
        public int TotalTarefas { get; set; }

        public Dictionary<string, int> TarefasPorClassificacao { get; set; } = new Dictionary<string, int>();

        public Dictionary<string, int> TarefasPorCliente { get; set; } = new Dictionary<string, int>();

        public int[] TarefasPorDiaSemana { get; set; } = new int[7];
    }
}
