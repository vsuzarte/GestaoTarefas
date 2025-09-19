using System.ComponentModel.DataAnnotations;

namespace TaskVitor.Models
{
    public class Lembrete
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descricao { get; set; }

        /// <summary>
        /// Data e hora do evento principal.
        /// </summary>
        [Required]
        public DateTime DataHora { get; set; }

        /// <summary>
        /// Quantos dias antes do evento o usuário deseja ser lembrado.
        /// Exemplo: 1 = um dia antes.
        /// </summary>
        [Required]
        public int DiasAntecedencia { get; set; } = 1;

        /// <summary>
        /// Indica se o lembrete já foi concluído.
        /// </summary>
        public bool Concluido { get; set; }
    }
}
