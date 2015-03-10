using Qvc.Executable;

namespace Qvc.Handler
{
    public interface IHandler
    {
    }

    public interface IHandleCommand<TCommand> : IHandler
        where TCommand : ICommand
    {
        void Handle(TCommand command);
    }

    public interface IHandleQuery<TQuery, TReturnType> : IHandler
        where TQuery : IQuery
    {
        TReturnType Handle(TQuery query);
    }
}