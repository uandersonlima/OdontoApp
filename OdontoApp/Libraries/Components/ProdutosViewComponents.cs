using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdontoApp.Models.Estoque;
using OdontoApp.Data;
using Microsoft.EntityFrameworkCore;

namespace OdontoApp.ViewComponents
{
    [ViewComponent(Name = "ProdutoList")]
    public class ProdutoListViewComponent : ViewComponent
    {
        private readonly OdontoAppContext _context;

        public ProdutoListViewComponent(OdontoAppContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string pesquisa)
        {
            var items = await GetItemsAsync(pesquisa);
            return View(items);
        }
        private Task<List<Produto>> GetItemsAsync(string pesquisa)
        {
            if (!string.IsNullOrEmpty(pesquisa))
            {
                return _context.Produto.Include(obj => obj.Estoques).Where(obj => obj.Descricao.Contains(pesquisa)).ToListAsync();
            }
            else
            {
                return _context.Produto.Include(obj => obj.Estoques).ToListAsync();
            }

        }
    }
}