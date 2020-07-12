using DomainEntities.Application;
using Library.Infrastructure;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PlacesApp
{
    public class MvcApplication : HttpApplication
    {
        private readonly string _credentialFile = "credentials.json";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // read credentials
            var jsonFileHandler = new JsonFileHandler();
            Globals.Credentials = jsonFileHandler.ReadJson<Credentials>(_credentialFile);
        }
    }
}
