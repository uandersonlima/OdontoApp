using Microsoft.AspNetCore.Mvc;
using OdontoApp.Libraries.Filtro;

namespace OdontoApp.Controllers
{
    [UserAuthorization]
    [AutoValidateAntiforgeryToken]
    public class EstoquesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}