namespace Virrum.Data
{
    using Autofac;

    using Virrum.Data.Contracts;
    using Virrum.Data.Extensions;

    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VirrumDbProvider>().As<IVirrumDbProvider>();
            builder.RegisterType<SystemTime>().As<ISystemTime>().SingleInstance();
        }
    }
}