using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcMembership;
using MvcMembership.Settings;

namespace goSSRA.Registration.Controllers
{
    [AuthorizeUnlessOnlyUser(Roles = "Admin")] // allows access if you're the only user, only validates role if role provider is enabled
    public class AdminSupportController : Controller
    {
        //
        // GET: /AdminSupport/
        public ActionResult Index()
        {
            return View();
        }
    }
}
