using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdontoApp.Libraries.Filtro;
using OdontoApp.Models.Const;
using OdontoApp.Models.Estoque;
using OdontoApp.Models.Helpers;
using OdontoApp.Models.ViewModel;
using OdontoApp.Services.Interfaces;
using System.Threading.Tasks;

namespace OdontoApp.Controllers
{
    [UserAuthorization]
    [AutoValidateAntiforgeryToken]
    public class EstoquesController : Controller
    {
        private readonly IEstoqueService estoqueSvc;
        private readonly IEntradaSaidaService entradasaidaSvc;
        private readonly IProdutoService produtoSvc;

        public EstoquesController(IEstoqueService estoqueSvc, IEntradaSaidaService entradasaidaSvc, IProdutoService produtoSvc)
        {
            this.estoqueSvc = estoqueSvc;
            this.entradasaidaSvc = entradasaidaSvc;
            this.produtoSvc = produtoSvc;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            EstoqueViewModel estoqueView = new EstoqueViewModel
            {
                Estoques = await estoqueSvc.GetAllAsync(),
                EntradaSaidas = await entradasaidaSvc.GetAllAsync(),
                Produtos = await produtoSvc.GetAllAsync()
            };

            return View(estoqueView);
        }

        [HttpGet]
        public async Task<IActionResult> ListaEstoque(AppView appview)
        {
            return PartialView(await  estoqueSvc.GetAllAsync(appview));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estoque = await estoqueSvc.GetByIdAsync(id.Value);

            if (estoque == null)
            {
                return NotFound();
            }

            return PartialView(estoque);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["ProdutoId"] = new SelectList(await produtoSvc.GetAllAsync(), "Id", "Descricao");
            return PartialView();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Estoque estoque)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create), estoque);
            }
            await estoqueSvc.AddAsync(estoque);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GerenciarEstoque(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estoque = await estoqueSvc.GetByIdAsync(id.Value);

            if (estoque == null)
            {
                return NotFound();
            }

            var entradasaida = new EntradaSaida
            {
                EstoqueId = estoque.EstoqueId,
                Estoque = estoque
            };

            return PartialView("_AddSub", entradasaida);
        }

        [HttpPost]
        public async Task<IActionResult> GerenciarEstoque(EntradaSaida entradasaida)
        {
            if (ModelState.IsValid)
            {
                if(entradasaida.TransactionType == TransactionType.Input)
                await entradasaidaSvc.AddProductAsync(entradasaida);

                if(entradasaida.TransactionType == TransactionType.Output)
                await entradasaidaSvc.SubstractProductAsync(entradasaida);

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(GerenciarEstoque), entradasaida);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estoque = await estoqueSvc.GetByIdAsync(id.Value);

            if (estoque == null)
            {
                return NotFound();
            }
            return PartialView(estoque);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Estoque estoque)
        {
            if (!ModelState.IsValid)
            {
                return View(estoque);
            }
            if (id != estoque.EstoqueId)
            {
                return NotFound();
            }

            await estoqueSvc.UpdateAsync(estoque);

            return RedirectToAction(nameof(Index));

        }

    }
}