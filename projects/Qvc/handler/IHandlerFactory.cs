namespace Qvc.Handler
{
    public interface IHandlerFactory
    {
        IHandler Create<THandler>() where THandler : IHandler;
    }
}
