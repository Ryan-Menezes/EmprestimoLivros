using EmprestimoLivros.DTO;
using EmprestimoLivros.Services.LoginService;
using EmprestimoLivros.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly ISessaoService _sessaoService;

        public LoginController(ILoginService loginService, ISessaoService sessaoService)
        {
            _loginService = loginService;
            _sessaoService = sessaoService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginDTO usuarioLoginDTO)
        {
            if (!ModelState.IsValid) return View(usuarioLoginDTO);

            var response = await _loginService.Login(usuarioLoginDTO);

            if (!response.Status)
            {
                TempData["erro"] = response.Mensagem;
                return View(usuarioLoginDTO);
            }

            TempData["sucesso"] = response.Mensagem;

            return RedirectToAction("Index", "Home");
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

        public IActionResult Logout()
        {
            _sessaoService.Remover();
            return RedirectToAction("Login");
        }
    }
}
