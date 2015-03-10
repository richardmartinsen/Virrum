namespace Virrum.Web.Utils
{
    using System.Web.Mvc;

    using Qvc.Handler;

    public class HandlerFactory : IHandlerFactory
    {
        public IHandler Create<THandler>() where THandler : IHandler
        {
            return DependencyResolver.Current.GetService<THandler>();
        }
    }
}