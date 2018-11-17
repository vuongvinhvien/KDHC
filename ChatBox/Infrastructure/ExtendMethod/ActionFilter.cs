using Autofac;
using Autofac.Integration.Mvc;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace Store.Web.Infrastructure.ExtensionMethod
{

    public class AllowIframeFromUriAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //...
            filterContext.HttpContext.Response.Headers.Remove("X-Frame-Options");
            base.OnResultExecuted(filterContext);
        }
    }
    public class CustomAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                                    || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);
            if (skipAuthorization)
            {
                return;
            }

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
            else
            {
                if (!HttpContext.Current.User.IsInRole(Roles))
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/Die.cshtml"
                    };
                    return;
                }
            }
        }
        public class HttpAjaxRequestAttribute : ActionMethodSelectorAttribute
        {
            public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
            {
                if (!controllerContext.HttpContext.Request.IsAjaxRequest())
                {
                    throw new Exception("This action " + methodInfo.Name + " can only be called via an Ajax request");
                }

                return true;
            }
        }

    }






}


