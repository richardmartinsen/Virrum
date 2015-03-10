namespace Qvc
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;

    using global::Qvc.Exception;

    using global::Qvc.Executable;

    using global::Qvc.Handler;

    using global::Qvc.Repository;

    using global::Qvc.Result;

    using global::Qvc.Validation;

    using global::Qvc.Validation.Metadata;

    public class Endpoint
    {
        private readonly IHandlerFactory _handlerFactory;

        private readonly TypeRepository _typeRepository;

        private readonly Action<string, System.Exception> _logErrorAction;

        private readonly ExecutableValidator _executableValidator;

        public Endpoint(IHandlerFactory handlerFactory, TypeRepository typeRepository, Action<string, System.Exception> logErrorAction)
        {
            _handlerFactory = handlerFactory;
            _typeRepository = typeRepository;
            _logErrorAction = logErrorAction;
            _executableValidator = new ExecutableValidator();
        }

        public ValidationConstraints Constraints(Type executable)
        {
            return _executableValidator.ValidationConstraints(executable);
        }

        public QueryResult Query(IQuery query)
        {
            try
            {
                var validationResult = Validate(query);
                return validationResult.IsValid ? Handle(query) : new QueryResult(validationResult);
            }
            catch (TargetInvocationException e)
            {
                var exception = e.GetBaseException();
                if (exception is InvalidException)
                {
                    return new QueryResult(new ValidationResult(exception as InvalidException));
                }

                if (exception is MultipleInvalidException)
                {
                    return new QueryResult(new ValidationResult(exception as MultipleInvalidException));
                }
                
                if (exception is UnauthorizedAccessException)
                {
                    return new QueryResult(new ValidationResult("You do not have access rights to get this data"));
                }

                _logErrorAction(string.Format("Query exception: {0}", query.GetType().FullName), exception);
                return new QueryResult(e.GetBaseException());
            }
            catch (System.Exception exception)
            {
                _logErrorAction(string.Format("Query exception: {0}", query.GetType().FullName), exception);
                return new QueryResult(exception);
            }
        }

        public CommandResult Command(ICommand command)
        {
            try
            {
                var validationResult = Validate(command);
                return validationResult.IsValid ? Handle(command) : new CommandResult(validationResult);
            }
            catch (TargetInvocationException e)
            {
                var exception = e.GetBaseException();
                if (exception is InvalidException)
                {
                    return new CommandResult(new ValidationResult(exception as InvalidException));
                }

                if (exception is MultipleInvalidException)
                {
                    return new CommandResult(new ValidationResult(exception as MultipleInvalidException));
                }

                if (exception is UnauthorizedAccessException)
                {
                    return new CommandResult(new ValidationResult("You do not have access rights to do this"));
                }

                _logErrorAction(string.Format("Command exception: {0}", command.GetType().FullName), exception);
                return new CommandResult(e.GetBaseException());
            }
            catch (System.Exception exception)
            {
                _logErrorAction(string.Format("Command exception: {0}", command.GetType().FullName), exception);
                return new CommandResult(exception);
            }
        }

        private static bool IsUserAuthorized(IHandler handler)
        {
            var authAttribute = handler.GetType().GetCustomAttributes().OfType<AuthorizeAttribute>().SingleOrDefault();
            return CheckAuthorizeAttribute(authAttribute);
        }

        private static bool IsUserAuthorized(MemberInfo handleMethod)
        {
            var authAttribute = handleMethod.GetCustomAttributes().OfType<AuthorizeAttribute>().SingleOrDefault();
            return CheckAuthorizeAttribute(authAttribute);
        }

        private static bool CheckAuthorizeAttribute(AuthorizeAttribute authAttribute)
        {
            if (authAttribute == null)
            {
                return true;
            }

            var roles = authAttribute.Roles.Split(',').Select(r => r.Trim()).ToList();

            if (roles.Any(role => HttpContext.Current.User.IsInRole(role)))
            {
                return true;
            }

            return false;
        }

        private ValidationResult Validate(IExecutable executable)
        {
            return _executableValidator.Validate(executable);
        }

        private CommandResult Handle(ICommand command)
        {
            var handler = CreateHandlerFromCommand(command);
            if (handler == null)
            {
                throw new FactoryFailedToBuildCommandHandler(command.GetType().Name);
            }

            if (!IsUserAuthorized(handler))
            {
                throw new UnauthorizedAccessException(command.GetType().Name);
            }

            var handleMethod = handler.GetType().GetMethod("Handle", new[] { command.GetType() });

            if (!IsUserAuthorized(handleMethod))
            {
                throw new UnauthorizedAccessException(command.GetType().Name);
            }

            handleMethod.Invoke(handler, new[] { command });

            return new CommandResult();
        }

        private QueryResult Handle(IQuery query)
        {
            var handler = CreateHandlerFromQuery(query);
            if (handler == null)
            {
                throw new FactoryFailedToBuildQueryHandler(query.GetType().Name);
            }

            if (!IsUserAuthorized(handler))
            {
                throw new UnauthorizedAccessException(query.GetType().Name);
            }

            var handleMethod = handler.GetType().GetMethod("Handle", new[] { query.GetType() });

            if (!IsUserAuthorized(handleMethod))
            {
                throw new UnauthorizedAccessException(query.GetType().Name);
            }

            var result = handler.GetType().GetMethod("Handle", new[] { query.GetType() }).Invoke(handler, new[] { query });
            return new QueryResult(result);
        }

        private IHandler CreateHandlerFromCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlerType = _typeRepository.GetCommandHandlerType(command);
            return (IHandler)CallGeneric(_handlerFactory, handlerType, "Create");
        }

        private IHandler CreateHandlerFromQuery<TQuery>(TQuery query) where TQuery : IQuery
        {
            var handlerType = _typeRepository.GetQueryHandlerType(query);
            return (IHandler)CallGeneric(_handlerFactory, handlerType, "Create");
        }

        private object CallGeneric<T>(T instance, Type type, string methodName, object[] parameters = null)
        {
            // Get the generic type definition
            var method = typeof(T).GetMethod(methodName);

            // Build a method with the specific type argument you're interested in
            method = method.MakeGenericMethod(type);

            // The "null" is because it's a static method
            return method.Invoke(instance, parameters ?? new object[0]);
        }
    }
}