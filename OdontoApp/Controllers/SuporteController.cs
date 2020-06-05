using Microsoft.AspNetCore.Mvc;

namespace OdontoApp.Controllers
{
    public class SuporteController : Controller
    {
        [HttpGet, Route("privacy")]
        public IActionResult Terms()
        {
            return View();
        }
    }
}