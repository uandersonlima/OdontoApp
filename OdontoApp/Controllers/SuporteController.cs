using Microsoft.AspNetCore.Mvc;

namespace OdontoApp.Controllers
{
    public class SuporteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}