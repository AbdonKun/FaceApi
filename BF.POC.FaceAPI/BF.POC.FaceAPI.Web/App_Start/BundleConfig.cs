using System.Web.Optimization;

namespace BF.POC.FaceAPI.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Styles Bundling
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css"
            ));

            // Scripts Bundling
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                "~/Scripts/umd/popper.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bffaceapi").Include(
                "~/Scripts/bf.faceapi.core.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/bffaceapi-groups").Include(
                "~/Scripts/bf.faceapi.groups.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/bffaceapi-people").Include(
                "~/Scripts/bf.faceapi.people.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/bffaceapi-faces").Include(
                "~/Scripts/bf.faceapi.faces.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/bffaceapi-test").Include(
                "~/Scripts/bf.faceapi.test.js"
            ));

        }
    }
}
