namespace Virrum.Web.App_Start
{
    using Autofac;

    using Virrum.Data;
    using Virrum.Data.Contracts;
    using Virrum.Data.Extensions;
    using Virrum.Users;
    using Virrum.Users.Contracts;

    public class IoCConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersService>().As<IUsersService>();
            builder.RegisterType<VirrumDbProvider>().As<IVirrumDbProvider>();
            builder.RegisterType<SystemTime>().As<ISystemTime>().SingleInstance();
        }
    }
}