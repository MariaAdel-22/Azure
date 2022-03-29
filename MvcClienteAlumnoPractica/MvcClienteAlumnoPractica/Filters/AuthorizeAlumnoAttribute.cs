using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;

namespace MvcClienteAlumnoPractica.Filters
{

    public class AuthorizeAlumnoAttribute : AuthorizeAttribute
    , System.Web.Http.Filters.IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated == false)
            {
                RouteValueDictionary routeLogin =
                    new RouteValueDictionary(new
                    {
                        controller = "Alumno",
                        action = "LogIn"
                    });
                RedirectToRouteResult result =
                    new RedirectToRouteResult(routeLogin);
                context.Result = result;
            }
        }
    }

}
