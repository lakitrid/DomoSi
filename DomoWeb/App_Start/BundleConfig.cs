using System.Web;
using System.Web.Optimization;

namespace DomoWeb
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-animate.js",
                        "~/Scripts/angular-cookies.js",
                        "~/Scripts/angular-route.js",
                        "~/Scripts/angular-sanitize.js",
                        "~/Scripts/angular-resource.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/angular-ui/ui-bootstrap.js",
                        "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                        "~/Scripts/angular-ui-router.js",
                        "~/Scripts/rdash/module.js",
                        "~/Scripts/rdash/directives/loading.js",
                        "~/Scripts/rdash/directives/widget-body.js",
                        "~/Scripts/rdash/directives/widget-footer.js",
                        "~/Scripts/rdash/directives/widget-header.js",
                        "~/Scripts/rdash/directives/widget.js"
                )); 

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/site.css",
                        "~/Content/rdash/css/rdash.css",
                        "~/Content/bootstrap-theme.css",
                        "~/Content/bootstrap.css",
                        "~/Content/font-awesome.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}