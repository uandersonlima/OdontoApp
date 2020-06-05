using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace OdontoApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ErrorController : Controller
    {

        [HttpGet, Route("Error/{statusCode}")]
        public IActionResult ErroGenerico(int statusCode)
        {
            ViewBag.StatusCode = statusCode;
            ViewBag.Mensagem = ReasonPhrases.GetReasonPhrase(statusCode);

            return View();
        }

        [HttpGet]
        public IActionResult Error500()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.Mensagem = exception.Error.Message;           
            return View();
        }

        [HttpGet, Route("Error/503")]
        public IActionResult Error503()
        {
            return View();
        }


        [HttpGet, Route("Error/404")]
        public IActionResult Error404()
        {
            return View();
        }

        [HttpGet, Route("Error/405")]
        public IActionResult Error405()
        {
            return View();
        }

        [HttpGet, Route("Error/403")]
        public IActionResult Error403()
        {
            return View();
        }

    }
}