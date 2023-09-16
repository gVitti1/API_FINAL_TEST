using System.ComponentModel.DataAnnotations;

namespace Gestao_API.Models
{
    public class Usuario
    {
        // Modelo de usuario

        [Required]
        [Key]
        public Guid UsuarioId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }

    }
}
