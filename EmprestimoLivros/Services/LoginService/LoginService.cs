using EmprestimoLivros.Data;
using EmprestimoLivros.DTO;
using EmprestimoLivros.Models;
using EmprestimoLivros.Services.SenhaService;

namespace EmprestimoLivros.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISenhaService _senhaService;

        public LoginService(ApplicationDbContext context, ISenhaService senhaService)
        {
            _context = context;
            _senhaService = senhaService;
        }

        public async Task<Response<Usuario>> Registrar(UsuarioRegisterDTO usuarioRegisterDTO)
        {

            Response<Usuario> response = new Response<Usuario>();

            try
            {
                if (VerificarSeEmailExiste(usuarioRegisterDTO))
                {
                    response.Mensagem = "Email já cadastrado";
                    response.Status = false;
                    return response;
                }

                _senhaService.CriarSenhaHash(usuarioRegisterDTO.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                Usuario usuario = new Usuario()
                {
                    Nome = usuarioRegisterDTO.Nome,
                    Sobrenome = usuarioRegisterDTO.Sobrenome,
                    Email = usuarioRegisterDTO.Email,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt,
                };

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                response.Mensagem = "Usuário cadastrado com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        private bool VerificarSeEmailExiste(UsuarioRegisterDTO usuarioRegisterDTO)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.Email == usuarioRegisterDTO.Email);

            return usuario != null;
        }
    }
}
