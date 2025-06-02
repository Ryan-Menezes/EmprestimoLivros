using ClosedXML.Excel;
using EmprestimoLivros.Data;
using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

            emprestimo.DataAtualizacao = DateTime.Now;

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

            var emprestimoDB = _db.Emprestimos.Find(emprestimo.Id);

            if (emprestimoDB == null) return NotFound();

            emprestimoDB.Fornecedor = emprestimo.Fornecedor;
            emprestimoDB.Recebedor = emprestimo.Recebedor;
            emprestimoDB.LivroEmprestado = emprestimo.LivroEmprestado;

            _db.Emprestimos.Update(emprestimoDB);
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

        public IActionResult Exportar()
        {
            var dados = GetDados();

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Dados empréstimo");

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);

                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Emprestimo.xls");
                }
            }
        }

        private DataTable GetDados()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados empréstimos";

            dataTable.Columns.Add("Recebedor", typeof(string));
            dataTable.Columns.Add("Fornecedor", typeof(string));
            dataTable.Columns.Add("Livro", typeof(string));
            dataTable.Columns.Add("Data empréstimo", typeof(DateTime));

            var emprestimos = _db.Emprestimos.ToList();

            if (emprestimos.Count == 0) return dataTable;

            emprestimos.ForEach(emprestimo =>
            {
                dataTable.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.LivroEmprestado, emprestimo.DataAtualizacao);
            });

            return dataTable;
        }
    }
}
