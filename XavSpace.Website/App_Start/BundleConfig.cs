using System.Web.Optimization;

namespace XavSpace.Website
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            #endregion

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            #region bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            #endregion

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                //"~/Content/bootstrap.css",
                      "~/Content/site.css")); //,

            bundles.Add(new ScriptBundle("~/bundles/WYSIWYG").Include(
                      "~/Scripts/bootstrap-wysiwyg.js",
                      "~/Scripts/jquery.hotkeys.js"));

            #region Typeahead
            // Add @Scripts.Render("~/bundles/typeahead") after jQuery in your _Layout.cshtml view
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/ta").Include("~/Scripts/typeahead.bundle*", "~/Scripts/layout/search.js"));
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/ta-bh").Include("~/Scripts/bloodhound*"));
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/ta-jquery").Include("~/Scripts/typeahead.jquery*"));
            #endregion

            bundles.Add(new ScriptBundle("~/bundles/blur").Include(
                      "~/Scripts/blur.js"));
        }
    }
}
