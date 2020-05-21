using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OdontoApp.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        #pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona
        public async Task<IViewComponentResult> InvokeAsync()
        #pragma warning restore CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona
        {
            return View();
        }
    }
}
