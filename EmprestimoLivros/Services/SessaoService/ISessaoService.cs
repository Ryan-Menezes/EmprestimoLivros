using EmprestimoLivros.Models;

namespace EmprestimoLivros.Services.SessaoService
{
    public interface ISessaoService
    {
        Usuario? Buscar();
        void Criar(Usuario usuario);
        void Remover();
    }
}
