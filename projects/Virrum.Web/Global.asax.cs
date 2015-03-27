using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;

namespace Virrum.Web
{
    using Autofac.Integration.Mvc;

    using Virrum.Data;
    using Virrum.Web.App_Start;
    using Virrum.Web.Utils;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = AutofacConfig.Init();
            //IoCConfig.RegisterInstances(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //// json serialization
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new FilteredCamelCasePropertyNamesContractResolver();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            };

            // Initialize database
            InitializeContext();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureBasedRazorViewEngine());
        }

        private static void InitializeContext()
        {
            DatabaseContext.Initialize(new DropCreateVirrumContextDebugInitializer(), true);
        }
    }
}
