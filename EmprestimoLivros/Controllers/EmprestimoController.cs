using EmprestimoLivros.Data;
using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmprestimoController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var emprestimos = _db.Emprestimos;

            return View(emprestimos);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Emprestimo emprestimo)
        {
            if (!ModelState.IsValid) return View();

            _db.Emprestimos.Add(emprestimo);
            _db.SaveChanges();

            TempData["sucesso"] = "Cadastro realizado com sucesso!";

            return RedirectToAction("Index");
        }

        public IActionResult Editar(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var emprestimo = _db.Emprestimos.FirstOrDefault(x => x.Id == id);

            if (emprestimo == null) return NotFound();

            return View(emprestimo);
        }

        [HttpPost]
        public IActionResult Editar([FromForm] Emprestimo emprestimo)
        {
            if (!ModelState.IsValid) return View();

            _db.Emprestimos.Update(emprestimo);
            _db.SaveChanges();

            TempData["sucesso"] = "Edição realizada com sucesso!";

            return RedirectToAction("Index");
        }

        public IActionResult Excluir(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var emprestimo = _db.Emprestimos.FirstOrDefault(x => x.Id == id);

            if (emprestimo == null) return NotFound();

            return View(emprestimo);
        }

        [HttpPost]
        public IActionResult Excluir([FromForm] Emprestimo emprestimo)
        {
            if (emprestimo == null) return NotFound();

            _db.Emprestimos.Remove(emprestimo);
            _db.SaveChanges();

            TempData["sucesso"] = "Exclusão realizada com sucesso!";

            return RedirectToAction("Index");
        }
    }
}
