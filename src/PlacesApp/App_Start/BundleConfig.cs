using System.Web;
using System.Web.Optimization;

namespace PlacesApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //  3rd party scripts and Application wide scripts
            bundles.Add(new ScriptBundle("~/bundles/script/common-script")
                   .Include(
                        "~/ViewResources/Common/Scripts/jquery-{version}.js",
                        "~/ViewResources/Common/Scripts/jquery.validate*",
                        "~/ViewResources/Common/Scripts/bootstrap.js"
                        )
                   .IncludeDirectory("~/ViewResources/Application/Scripts/", "*.js", true)
                   );


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/script/modernizr")
                   .Include("~/ViewResources/Common/Scripts/modernizr-*")
                   );

            //  Page  scripts
            //bundles.Add(new ScriptBundle("~/bundles/place-script")
            //       .IncludeDirectory("~/ViewResources/Application/Scripts/", "*.js", true)
            //       );


            //  3rd party and Application styles
            bundles.Add(new StyleBundle("~/bundles/Content/css")
                   .Include(
                      "~/ViewResources/Common/Content/bootstrap.css",
                      "~/ViewResources/Application/Content/site.css"
                      ));
        }
    }
}
