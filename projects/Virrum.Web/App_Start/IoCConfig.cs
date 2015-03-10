namespace Virrum.Web.App_Start
{
    using Autofac;

    using Virrum.Data;
    using Virrum.Data.Contracts;
    using Virrum.Data.Extensions;
    using Virrum.Home;
    using Virrum.Home.Contracts;

    public class IoCConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HomeService>().As<IHomeService>();
            builder.RegisterType<VirrumDbProvider>().As<IVirrumDbProvider>();
            builder.RegisterType<SystemTime>().As<ISystemTime>().SingleInstance();
        }
    }
}