using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OdontoApp.Models.Const;
using OdontoApp.Services.Interfaces;
using System;

namespace OdontoApp.Libraries.Filtro
{
    public sealed class PaymentAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private IAuthService authService;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            authService = (IAuthService)context.HttpContext.RequestServices.GetService(typeof(IAuthService));
            var user = authService.GetLoggedUserAsync().Result;
            if (user.AccessType != AccessType.Administrator && (string.IsNullOrEmpty(user.PaymentStatus) ||
                string.Compare(user.PaymentStatus.Trim(), "canceled") == 0))
            {
                context.Result = new RedirectToActionResult("DadosDoCartao", "Pagamentos", null);
            }
        }

    }
}
