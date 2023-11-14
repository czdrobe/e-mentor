using meditatii.Models;
using Meditatii.Core;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using Meditatii.Core.Enums;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using meditatii.web.Utils;
using Meditatii.CoreNew.Enums;
using Stripe;
using System.Linq;
using System.Web.Hosting;

namespace meditatii.Controllers
{
    public class WebhookController : ApiController
    {
        private IUsersService usersService;
        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        const string endpointSecret = "whsec_UhIpuSNVsVjl8rJcw5uiNnfxmTXCK8Qv";
        public WebhookController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpPost]
        [Route("webhook")]
        public async Task webhook()
        {
            string json =await Request.Content.ReadAsStringAsync();

            var path = HostingEnvironment.MapPath("~") + "notification.log";
            System.IO.File.AppendAllLines(path, new string[] { "webhook: " + DateTime.Now.ToString() + ":json:" + json });

            var headers = Request.Headers;
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, headers.GetValues("Stripe-Signature").First() , endpointSecret);

                // Handle the event
                // Handle the checkout.session.completed event
                if (stripeEvent.Type == Events.ChargeSucceeded ||
                    stripeEvent.Type == Events.ChargeUpdated)
                {
                    var charge = stripeEvent.Data.Object as Stripe.Charge;
                    var email = charge.BillingDetails.Email;
                    System.IO.File.AppendAllLines(path, new string[] { "email: " + email });
                    User u = usersService.GetUser(email);

                    System.IO.File.AppendAllLines(path, new string[] { "user id: " + u.Id});

                    usersService.UpdateSubscriptionPeriod(u.Id, 365);
                }
                
                //return Redirect("/u/profile?refreshuser=1");
            }
            catch (StripeException e)
            {
                //return View();
                System.IO.File.AppendAllLines(path, new string[] { "Exception: " + e.Message + ":StripeResponse:" + e.StripeResponse + ":StripeError:" + e.StripeError });

            }
        }
    }

}