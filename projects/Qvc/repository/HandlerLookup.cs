using System;
using System.Collections.Generic;
using System.Linq;

using Qvc.Handler;

namespace Qvc.Repository
{
    public class HandlerLookup
    {
        private readonly IDictionary<Type, Type> _commandMappings;

        private readonly IDictionary<Type, Type> _queryMappings;

        public HandlerLookup(IEnumerable<Type> types)
        {
            var commandHandlers = FindTypesImplementingInterface(types, typeof(IHandleCommand<>));
            var queryHandlers = FindTypesImplementingInterface(types, typeof(IHandleQuery<,>));

            var commandHandlerTypes =
                commandHandlers.SelectMany(handler => GetHandledCommandsByType(handler).Select(t => new Tuple<Type, Type>(t, handler))).ToList();
            _commandMappings = commandHandlerTypes.ToDictionary(t => t.Item1, t => t.Item2);

            var queryHandlerTypes =
                queryHandlers.SelectMany(handler => GetHandledQueriesByType(handler).Select(t => new Tuple<Type, Type>(t, handler))).ToList();
            _queryMappings = queryHandlerTypes.ToDictionary(t => t.Item1, t => t.Item2);
        }

        public Type FindHandlerForCommand(Type command)
        {
            return _commandMappings[command];
        }

        public Type FindHandlerForQuery(Type query)
        {
            return _queryMappings[query];
        }

        private IEnumerable<Type> FindTypesImplementingInterface(IEnumerable<Type> allTypes, Type interfaceType)
        {
            return allTypes.Where(p => DoesTypeSupportInterface(p, interfaceType) && p.IsClass);
        }

        private bool DoesTypeSupportInterface(Type type, Type inter)
        {
            if (inter.IsAssignableFrom(type))
            {
                return true;
            }
            if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == inter))
            {
                return true;
            }
            return false;
        }

        private IEnumerable<Type> GetHandledCommandsByType(Type type)
        {
            return GetHandledObjectsByType(type, typeof(IHandleCommand<>));
        }

        private IEnumerable<Type> GetHandledQueriesByType(Type type)
        {
            return GetHandledObjectsByType(type, typeof(IHandleQuery<,>));
        }

        private IEnumerable<Type> GetHandledObjectsByType(Type type, Type handlerInterface)
        {
            var interfaces = type.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface);
            var types = interfaces.Select(t => t.GenericTypeArguments.FirstOrDefault());
            return types.Where(t => t != null).ToList();
        }
    }
}