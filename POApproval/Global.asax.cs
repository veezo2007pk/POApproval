using POApproval.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace POApproval
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Session_End()
        {
            Session.Abandon();
      
           
        }
        protected void Session_Start()
        {
            if (Session["intUserCode"] == null)
            {
                new RedirectToRouteResult(new RouteValueDictionary { { "action", "Login" }, { "controller", "Account" } });


            }
            
        }
        protected void Application_EndRequest()
        {
            

            if (Context.Response.StatusCode == 404)
            {
                Response.Clear();

                var rd = new RouteData();
                //rd.DataTokens["area"] = "AreaName"; // In case controller is in another area
                rd.Values["controller"] = "Errors";
                rd.Values["action"] = "Error404";

                IController c = new ErrorsController();
                c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
            }
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
