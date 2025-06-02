using System.ComponentModel.DataAnnotations;

namespace EmprestimoLivros.DTO
{
    public class UsuarioRegisterDTO
    {
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public string Sobrenome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Esse campo é obrigatório"), Compare("Senha", ErrorMessage = "As senhas são diferentes")]
        public string ConfirmarSenha { get; set; } = string.Empty;
    }
}
