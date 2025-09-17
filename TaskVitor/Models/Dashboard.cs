namespace TaskVitor.Models
{
    public class Dashboard
    {
        public int TotalTarefas { get; set; }

        public Dictionary<string, int> TarefasPorClassificacao { get; set; } = new Dictionary<string, int>();

        public Dictionary<string, int> TarefasPorCliente { get; set; } = new Dictionary<string, int>();

        public Dictionary<string, int> TarefasPorProjeto { get; set; } = new Dictionary<string, int>();

        public int[] TarefasPorDiaSemana { get; set; } = new int[7];

        public string ProjetoComMaisTarefas { get; set; }

        public int QuantidadeTarefasProjeto { get; set; }

        public string DiaMaisProdutivo { get; set; }

        public int QuantidadeTarefasDiaMaisProdutivo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }
    }
}
