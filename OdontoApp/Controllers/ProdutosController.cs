using Microsoft.AspNetCore.Mvc;
using OdontoApp.Libraries.Filtro;

namespace OdontoApp.Controllers
{
    [AutoValidateAntiforgeryToken, UserAuthorization]
    public class ProdutosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}