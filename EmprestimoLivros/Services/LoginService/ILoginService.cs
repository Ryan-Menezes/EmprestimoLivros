using EmprestimoLivros.DTO;
using EmprestimoLivros.Models;

namespace EmprestimoLivros.Services.LoginService
{
    public interface ILoginService
    {
        Task<Response<Usuario>> Login(UsuarioLoginDTO usuarioLoginDTO);
        Task<Response<Usuario>> Registrar(UsuarioRegisterDTO usuarioRegisterDTO);
    }
}
