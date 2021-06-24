using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace POApproval.GlobalInfo
{
    public class SessionAuthorizeAttributeForUsers : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return Convert.ToBoolean( httpContext.Session["bolIsNewUser"]) == true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                                  new RouteValueDictionary
                                  {
                                   { "action", "Login" },
                                   { "controller", "Account" }
                                  });
        }
    }
}