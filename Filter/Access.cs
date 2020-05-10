using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TSL__Quiz.Models;

namespace TSL__Quiz.Filter
{
    public class Access : ActionFilterAttribute, IAuthorizationFilter
    {
        LocationDetails loc = new LocationDetails();
        public void OnAuthorization(AuthorizationContext context)
        {
            string option = HttpContext.Current.Request["option"];

            if (option==null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Home" },
                        { "action", "Index" }
                    }
                );
            }
            //if(int.Parse(loc.Distance)>3)
            //{
                
            //}
        }
    }
}