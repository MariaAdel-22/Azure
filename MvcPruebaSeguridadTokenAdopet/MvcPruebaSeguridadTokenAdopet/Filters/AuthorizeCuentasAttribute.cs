﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPruebaSeguridadTokenAdopet.Filters
{
    public class AuthorizeCuentasAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity.IsAuthenticated == false)
            {

                RouteValueDictionary routeLogin = new RouteValueDictionary(new
                {

                    controller = "Home",
                    action = "Index"
                });

                RedirectToRouteResult result = new RedirectToRouteResult(routeLogin);

                context.Result = result;

            }

        }
    }
}