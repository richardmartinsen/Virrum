namespace Virrum.Web
{
    using System.Reflection;
    using System.Web.Http;

    using Autofac;

    using Virrum.Web.Utils;

    using log4net;

    using Qvc.Handler;

    public class QvcModule : Autofac.Module
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(type => typeof(IHandler).IsAssignableFrom(type)).AsSelf();

            var qvcEndpoint = Qvc.Qvc.Start(new HandlerFactory(), new[] { Assembly.GetExecutingAssembly() }, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings, (errorMessage, exception) => Log.Error(errorMessage, exception));
            builder.RegisterInstance(qvcEndpoint);
        }
    }
}