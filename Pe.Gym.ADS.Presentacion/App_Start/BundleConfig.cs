using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace GDirectiva.Presentacion.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));*/

            bundles.Add(new ScriptBundle("~/Components/JQuery").Include(
                        "~/Components/JQuery/jquery-1.11.2.js",
                        "~/Components/JQuery/jquery-ui-1.10.4.custom.js",
                        "~/Components/JQuery/jquery.validate.js",
                        "~/Components/JQuery/jquery.validate.additional-methods.js",
                        "~/Components/JQuery/jquery.mask.js",
                        "~/Components/JQuery/jquery.mask.min.js"
                //"~/Components/JQuery/jquery.ptTimeSelect.js",                        
                //"~/Components/JQuery/moment.min.js",
                //"~/Components/JQuery/pikaday.js"
            ));

            bundles.Add(new ScriptBundle("~/Components/DataTables").Include(
                        "~/Components/DataTables/js/jquery.dataTables.js",
                        "~/Components/DataTables/js/dataTables.responsive.js"
                        , "~/Components/DataTables/js/dataTables.bootstrap.js"
            ));

            bundles.Add(new ScriptBundle("~/Components/Codemaleon").Include(
                        "~/Components/Codemaleon/ns.js"
            ));

            bundles.Add(new ScriptBundle("~/Components/Gmd").Include(
                      "~/Scripts/Base/Layout/Util.js",
                        "~/Components/Gmd/Ajax/Ajax.js",
                        "~/Components/Gmd/ProgressBar/ProgressBar.js",
                        "~/Components/Gmd/Validator/Validator.js",
                        "~/Components/Gmd/Dialog/Dialog.js",
                        "~/Components/Gmd/Message/Message.js",
                        "~/Components/Gmd/Storage/Storage.js",
                        "~/Components/Gmd/TextBox/TextBox.js",
                        "~/Components/Gmd/Calendar/Calendar.js",
                        "~/Components/Gmd/Grid/Grid.js",
                        "~/Components/Gmd/Chart/Chart.js"
            ));

            bundles.Add(new ScriptBundle("~/FrameworkStyle/js").Include(
                        "~/Components/Bootstrap/js/bootstrap.js",
                        "~/Components/Bootstrap/bootstrap-datetimepicker/moment.js",
                        "~/Components/Bootstrap/bootstrap-datetimepicker/bootstrap-datetimepicker.js"
            ));


            bundles.Add(new StyleBundle("~/Components/GmdCss").Include(
                        "~/Components/Gmd/ProgressBar/ProgressBar.css",
                        "~/Components/Gmd/Dialog/Dialog.css",
                        "~/Components/Gmd/Message/Message.css"
            ));

            bundles.Add(new StyleBundle("~/Components/JQueryCss").Include(
            "~/Components/JQuery/jquery-ui-1.10.0.custom.css"
            ));

            bundles.Add(new ScriptBundle("~/Components/DataTablesCss").Include(
                        "~/Components/DataTables/css/jquery.dataTables.css"
                        , "~/Components/DataTables/css/dataTables.bootstrap.css"
                        , "~/Components/DataTables/css/dataTables.responsive.css"
            ));

            //bundles.Add(new StyleBundle("~/Template/css").Include(
            //            "~/Theme/comp.css",
            //            "~/Theme/style.css",
            //            "~/Theme/skin.css"));
            bundles.Add(new StyleBundle("~/Template/css").Include(
                        "~/Theme/app/main.css",
                        "~/Theme/app/app/box.css",
                        "~/Theme/app/layout.css",
                        "~/Theme/app/form.css",
                        "~/Theme/app/table.css",
                        "~/Theme/app/comp.css",
                        "~/Theme/app/nav.css",
                        "~/Theme/app/uiExt.css",
                        "~/Theme/app/utilities.css",
                        "~/Theme/app/responsive.css",
                        "~/Theme/app/skin.css"
            ));


            bundles.Add(new StyleBundle("~/FrameworkStyle/css").Include(
                        "~/Components/Bootstrap/css/bootstrap.css",
                        "~/Components/font-awesome/css/font-awesome.css",
                        "~/Components/Bootstrap/bootstrap-datetimepicker/bootstrap-datetimepicker.min.css"
            ));

            bundles.Add(new ScriptBundle("~/Components/Charts").Include(
                        "~/Components/Highcharts/js/highcharts.src.js",
                        "~/Components/Highcharts/js/highcharts-more.js"
            ));

            var directoryScripts = HttpContext.Current.Server.MapPath("~/Scripts");
            GenerateDynamicScriptBundle(bundles, new DirectoryInfo(directoryScripts));

        }
        private static void GenerateDynamicScriptBundle(BundleCollection bundles, DirectoryInfo directory, string pathDirectories = "")
        {
            var files = directory.EnumerateFiles();
            if (files != null && files.Any())
            {
                bundles.Add(new ScriptBundle("~/Scripts/" + pathDirectories.Replace("/", "").ToLower()).Include(
                                        "~/Scripts/" + pathDirectories + "*.js"));
            }
            var directories = directory.EnumerateDirectories().ToList();
            if (directories != null && directories.Any())
            {
                directories.ForEach(d =>
                {
                    GenerateDynamicScriptBundle(bundles, d, (pathDirectories + d.Name + "/"));
                });
            }
        }
    }
}
