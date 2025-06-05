using System.ComponentModel.DataAnnotations;

namespace EmprestimoLivros.DTO
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public string Senha { get; set; } = string.Empty;
    }
}
