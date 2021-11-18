using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace meditatii
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();


            routes.MapRoute(
                name: "RequestsSubpages",
                url: "solicitare-de-meditatii/{*subpage}",
                defaults: new { controller = "Requests", action = "Index" }
            );

            routes.MapRoute(
                name: "User",
                url: "u/{*subpage}",
                defaults: new { controller = "U", action = "Index" }
            );
            routes.MapRoute(
                name: "Teacher",
                url: "teacher",
                defaults: new { controller = "Teacher", action = "Index" }
            );
            routes.MapRoute(
                name: "TeacherProfile",
                url: "teacherprofile/{id}",
                defaults: new { controller = "TeacherProfile", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Room",
                url: "room/{appoitmentid}",
                defaults: new { controller = "Room", action = "Index", appoitmentid = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "PaymentConfirm",
                url: "payment/confirm",
                defaults: new { controller = "Payment", action = "confirm" }
            );
            routes.MapRoute(
                name: "PaymentThankYou",
                url: "payment/thank-you",
                defaults: new { controller = "Payment", action = "thank-you" }
            );
            routes.MapRoute(
                name: "DownloadInvoice",
                url: "payment/download/{paymentCode}",
                defaults: new { controller = "Payment", action = "download", paymentCode = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Requests",
                url: "solicitare-de-meditatii",
                defaults: new { controller = "Requests", action = "Index" }
            );
            routes.MapRoute(
                name: "NewRequest",
                url: "adauga-solicitare-noua",
                defaults: new { controller = "Requests", action = "Index" }
            );
            routes.MapRoute(
                name: "Payment",
                url: "payment/{period}",
                defaults: new { controller = "Payment", action = "Index", appoitmentsid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Abonamente",
                url: "abonamente",
                defaults: new { controller = "Abonamente", action = "Index" }
            );

            routes.MapRoute(
                "OnlyAction",
                "{action}",
                new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
