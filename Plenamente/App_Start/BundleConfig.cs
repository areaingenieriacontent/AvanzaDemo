using System.Web.Optimization;

namespace Plenamente
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            // Custom chartjs
            bundles.Add(new ScriptBundle("~/bundles/chartjs").Include(
                    "~/Scripts/chartjs/Chart.min.js",
                    "~/Scripts/chartjs/Chart.bundle.min.js",
                    "~/Scripts/chartjs/script-custom-chart.js"));

            // Custom Calendar.
            bundles.Add(new ScriptBundle("~/bundles/Script-calendar").Include(
                               "~/Scripts/moment.min.js",
                               "~/Scripts/fullcalendar.min.js",
                               "~/Scripts/fullcalendar.print.min.css",
                               "~/Scripts/locale/es.js",
                               "~/Scripts/script-custom-calendar.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
