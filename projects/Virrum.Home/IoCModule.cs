namespace Virrum.Home
{
    using Autofac;

    using Virrum.Home.Contracts;

    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HomeService>().As<IHomeService>();
        }
    }

}