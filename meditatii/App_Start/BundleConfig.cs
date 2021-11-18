using System.Web;
using System.Web.Optimization;

namespace meditatii
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/bundles/calendar").Include(
            "~/Scripts/jquery-{version}.js",
            "~/Scripts/moment.min.js",
            "~/Scripts/fullcalendar.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/popper.min.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"
                ));

            bundles.Add(new StyleBundle("~/css/css").Include(
          "~/css/fullcalendar.css",
          "~/css/bootstrap.css",
          "~/css/styles.css"
          ));

#if DEBUG
        BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif

        }
    }
}
