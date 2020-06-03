using Microsoft.AspNetCore.Mvc;
using OdontoApp.Libraries.Filtro;

namespace OdontoApp.Controllers
{
    [UserAuthorization]
    [AutoValidateAntiforgeryToken]
    public class EntradaSaidaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}