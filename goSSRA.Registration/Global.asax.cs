using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using goSSRA.Registration.Models;

namespace goSSRA.Registration
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/fwlink/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // ***  to due - disable database seed option
            //  Database.SetInitializer <RegistrationDB> (new RegistrationDBInitializer());
        }

        protected void Application_AuthenticateRequest()
        {
            if (HttpContext.Current.User != null)
                Membership.GetUser(true);
        }
    }
}
