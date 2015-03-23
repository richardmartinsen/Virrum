namespace Virrum.Users
{
    using Autofac;

    using Virrum.Users.Contracts;

    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersService>().As<IUsersService>();
        }
    }

}