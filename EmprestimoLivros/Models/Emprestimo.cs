using System.ComponentModel.DataAnnotations;

namespace EmprestimoLivros.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public string Recebedor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public string Fornecedor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public string LivroEmprestado {  get; set; } = string.Empty;

        public DateTime DataAtualizacao { get; set; }
    }
}
