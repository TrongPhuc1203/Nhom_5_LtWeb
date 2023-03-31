using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;

namespace MVC_Project
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorizeAttribute: AuthorizeAttribute
    {
        public string ViewName { get; set; }
        public CustomAuthorizeAttribute() 
        {
            ViewName = "AuthorizeFailed";
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            IsUserAuthorized(filterContext);
        }
        void IsUserAuthorized(AuthorizationContext filterContext)
        {
            //user is Authorized
            if (filterContext.Result == null)
                return;
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                ViewDataDictionary dic = new ViewDataDictionary();
                dic.Add("Message", "You don't have sufficient privleges for this operation");
                var result = new ViewResult() { ViewName=this.ViewName,ViewData = dic};
                filterContext.Result = result;
            }
        }
    }
}