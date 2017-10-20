using GuitarStore.Domain.Entities;
using GuitarStore.WebUi.Binders;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GuitarStore.WebUi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MvcHandler.DisableMvcResponseHeader = true;

            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
