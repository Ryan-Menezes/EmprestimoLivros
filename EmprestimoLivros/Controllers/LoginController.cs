using EmprestimoLivros.DTO;
using EmprestimoLivros.Services.LoginService;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioRegisterDTO usuarioRegisterDTO)
        {
            if (!ModelState.IsValid) return View(usuarioRegisterDTO);

            var response = await _loginService.Registrar(usuarioRegisterDTO);

            if (!response.Status)
            {
                TempData["erro"] = response.Mensagem;
                return View(usuarioRegisterDTO);
            }

            TempData["sucesso"] = response.Mensagem;

            return RedirectToAction("Index");
        }
    }
}
