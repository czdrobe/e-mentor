using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace meditatii.web.Filters
{
    public class AllowedIPAttribute : ActionFilterAttribute
    {

        //overrinding OnActionExecuting method to check Ip, before any code from Action is executed.
        /*public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Retrieve user's IP
            string usersIpAddress = HttpContext.Current.Request.UserHostAddress;

            if (!checkIp(usersIpAddress))
            {
                //return to work in progress
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "workinprogress" }
                });
            }

            base.OnActionExecuting(filterContext);
        }*/



        public static bool checkIp(string usersIpAddress)
        {
            //get allowedIps Setting from Web.Config file and remove whitespaces from int
            string allowedIps = ConfigurationManager.AppSettings["allowedIPs"].Replace(" ", "").Trim();


            //convert allowedIPs string to table by exploding string with ';' delimiter
            string[] ips = allowedIps.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            //iterate ips table
            foreach (var ip in ips)
            {
                if (ip.Equals(usersIpAddress))
                    return true; //return true confirming that user's address is allowed
            }

            //if we get to this point, that means that user's address is not allowed, therefore returning false
            return false;

        }
    }
}