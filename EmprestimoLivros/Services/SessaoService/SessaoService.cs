using EmprestimoLivros.Models;
using Newtonsoft.Json;

namespace EmprestimoLivros.Services.SessaoService
{
    public class SessaoService : ISessaoService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SessaoService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Usuario? Buscar()
        {
            var usuarioJson = _contextAccessor?.HttpContext?.Session.GetString("sessaoUsuario");

            if (string.IsNullOrEmpty(usuarioJson)) return null;

            return JsonConvert.DeserializeObject<Usuario>(usuarioJson);
        }

        public void Criar(Usuario usuario)
        {
            var usuarioJson = JsonConvert.SerializeObject(usuario);

            _contextAccessor?.HttpContext?.Session.SetString("sessaoUsuario", usuarioJson);
        }

        public void Remover()
        {
            _contextAccessor?.HttpContext?.Session.Remove("sessaoUsuario");
        }
    }
}
