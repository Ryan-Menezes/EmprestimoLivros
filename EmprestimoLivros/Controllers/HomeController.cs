using EmprestimoLivros.Models;
using EmprestimoLivros.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmprestimoLivros.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISessaoService _sessaoService;

        public HomeController(ILogger<HomeController> logger, ISessaoService sessaoService)
        {
            _logger = logger;
            _sessaoService = sessaoService;
        }

        public IActionResult Index()
        {
            if (_sessaoService.Buscar() == null) return RedirectToAction("Login", "Login");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
