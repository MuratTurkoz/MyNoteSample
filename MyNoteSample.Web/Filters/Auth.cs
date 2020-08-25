using MyNoteSample.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNoteSample.Web.Filters
{
    public class Auth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.user == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }

        }
    }
}