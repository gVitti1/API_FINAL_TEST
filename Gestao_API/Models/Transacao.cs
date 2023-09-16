using System.ComponentModel.DataAnnotations;

namespace Gestao_API.Models
{
    public class Transacao
    {
        //Modelo de transacao

        [Required]
        [Key]
        public Guid TransacaoId { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public DateTime HoraLancamento { get; set; }
        [Required]
        public Guid UsuarioId { get; set; }
        [Required]
        public Guid TransferenciaId { get; set; }
    }
}

