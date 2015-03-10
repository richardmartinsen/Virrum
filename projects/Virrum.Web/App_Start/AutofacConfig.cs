namespace Virrum.Web
{
    using System.Linq;
    using System.Reflection;
    using System.Web.Compilation;

    using Autofac;
    using Autofac.Integration.Mvc;

    public static class AutofacConfig
    {
        public static IContainer Container { get; private set; }

        public static IContainer Init()
        {
            if (Container != null)
            {
                return Container;
            }

            lock (typeof(AutofacConfig))
            {
                if (Container != null)
                {
                    return Container;
                }

                var builder = new ContainerBuilder();
                var executingAssembly = Assembly.GetExecutingAssembly();
                builder.RegisterControllers(executingAssembly).InstancePerRequest();
                builder.RegisterSource(new ViewRegistrationSource());
                builder.RegisterModule<AutofacWebTypesModule>();
                var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(a => a.FullName.Contains("Virrum")).ToArray();
                builder.RegisterAssemblyModules(assemblies);

                Container = builder.Build();

                return Container;
            }
        }
    }
}