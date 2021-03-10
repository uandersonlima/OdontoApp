using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OdontoApp.Models.Const;
using OdontoApp.Services.Interfaces;

namespace OdontoApp.Libraries.Filtro
{
    public sealed class UserAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string accessType;
        private IAuthService authService;
        public UserAuthorizationAttribute(string accessType = AccessType.User)
        {
            this.accessType = accessType;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            authService = (IAuthService)context.HttpContext.RequestServices.GetService(typeof(IAuthService));
            var user = authService.GetLoggedUserAsync().Result;
            if (user == null || (user.AccessType == AccessType.User && accessType == AccessType.Administrator))
            {
                context.Result = new RedirectToActionResult("signin", "auth", null);//StatusCodeResult(403);
            }
            else if (!user.EmailConfirmed)
            {
                context.Result = new RedirectToActionResult("confirmaremail", "Usuarios", null);
            }
        }
    }
}
