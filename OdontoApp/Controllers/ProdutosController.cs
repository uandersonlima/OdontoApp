using Microsoft.AspNetCore.Mvc;
using OdontoApp.Libraries.Filtro;
using OdontoApp.Models.Estoque;
using OdontoApp.Services.Interfaces;
using System.Threading.Tasks;

namespace OdontoApp.Controllers
{
    [AutoValidateAntiforgeryToken, UserAuthorization]
    public class ProdutosController : Controller
    {
        private readonly IProdutoService produtoSvc;

        public ProdutosController(IProdutoService produtoSvc)
        {
            this.produtoSvc = produtoSvc;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await produtoSvc.GetByIdAsync(id.Value);

            if (produto == null)
            {
                return NotFound();
            }

            return PartialView("_details", produto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_create");
        }


        [HttpPost]
        public async Task<IActionResult> Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                await produtoSvc.AddAsync(produto);

                return RedirectToAction("Index", "Estoques");
            }
            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await produtoSvc.GetByIdAsync(id.Value);

            if (produto == null)
            {
                return NotFound();
            }
            return PartialView("_edit", produto);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await produtoSvc.UpdateAsync(produto);

                return RedirectToAction("Index", "Estoques");
            }
            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await produtoSvc.GetByIdAsync(id.Value);

            if (produto == null)
            {
                return NotFound();
            }

            return PartialView("_delete", produto);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await produtoSvc.DeleteAsync(id);
            return RedirectToAction("Index", "Estoques");
        }
    }
}