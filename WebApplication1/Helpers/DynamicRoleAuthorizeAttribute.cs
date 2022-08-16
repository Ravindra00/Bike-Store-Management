using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Helpers
{
    public class DynamicRoleAuthorizeAttribute: AuthorizeAttribute
    {
        //IMyHelper myHelper=new MyHelper();
        //MyHelper myHelper = new MyHelper();
        //MyHelper myHelper;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var controller = httpContext.Request.RequestContext
                .RouteData.GetRequiredString("controller");
            var action = httpContext.Request.RequestContext
                .RouteData.GetRequiredString("action");
            // feed the roles here
            Roles = string.Join(",", MyHelper.Get(controller, action));
            return base.AuthorizeCore(httpContext);
        }
    }
}