using meditatii.Models;
using Meditatii.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace meditatii.Controllers
{
    public class OldBaseController : Controller
    {
        private LazyLoadProvider lazyProvider;

        private IUsersService userService;


        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController"/> class.
        /// The base constructor allows injected service factories to be passed in to be instantiated when needed (this is primarily a code cleanliness concern...)
        /// </summary>
        /// <param name="serviceFactories">param array of service factories availabled to the class</param>
        protected OldBaseController(params Func<ILazyLoadable>[] serviceFactories)
        {
            lazyProvider = new LazyLoadProvider(serviceFactories);
        }

        /// <summary>
        /// Retrieve a service by type. If it's already been instantiated we'll use that, otherwise the factory will be called
        /// </summary>
        /// <typeparam name="T">The type of the service to retrieve - must defive from ILazyLoadable</typeparam>
        /// <returns>The instantiated service</returns>
        protected T GetService<T>()
            where T : ILazyLoadable
        {
            return (T)lazyProvider.GetService<T>();
        }
        /*
        public UserModel CurrentUser
        {
            get
            {
          
                if (HttpContext.User.Identity.Name == null)
                    return null;
                if (Session["CurrentUser"] != null)
                    return (UserModel)Session["CurrentUser"];

                lazyProvider.GetService 
                Session["CurrentUser"] = userService.GetUser(HttpContext.User.Identity.Name);

                return (UserModel)Session["CurrentUser"];
            }

            set { Session["CurrentUser"] = value; }
        }*/
    }
}