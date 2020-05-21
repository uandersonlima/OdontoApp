using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OdontoApp.Models.Const;
using OdontoApp.Services.Interfaces;
using System;

namespace OdontoApp.Libraries.Filtro
{
    public sealed class PaymentAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private ILoginService loginSvc;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            loginSvc = (ILoginService)context.HttpContext.RequestServices.GetService(typeof(ILoginService));
            var user = loginSvc.GetUser();
            if (user.AccessType != AccessType.Administrator && (string.IsNullOrEmpty(user.PaymentStatus) ||
                string.Compare(user.PaymentStatus.Trim(), "canceled") == 0))
            {
                context.Result = new RedirectToActionResult("DadosDoCartao", "Clientes", null);
            }
        }

    }
}
