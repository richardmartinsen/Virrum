using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Qvc.Exception;
using Qvc.Executable;

namespace Qvc.Repository
{
    public class TypeRepository
    {
        private readonly IEnumerable<Type> _commands;

        private readonly IEnumerable<Type> _queries;

        private readonly HandlerLookup _handlers;

        public TypeRepository(IEnumerable<Assembly> assemblies)
        {
            var allTypes = assemblies.SelectMany(s => s.GetTypes()).ToList();

            _commands = FindTypesImplementingInterface(allTypes, typeof(ICommand)).ToList();
            _queries = FindTypesImplementingInterface(allTypes, typeof(IQuery)).ToList();

            _handlers = new HandlerLookup(allTypes);
        }

        public Type GetCommand(string commandName)
        {
            var commands = _commands.Where(p => p.Name.EndsWith(commandName)).ToList();

            if (!commands.Any())
            {
                throw new CommandDoesNotExistException(commandName);
            }
            if (commands.Count() > 1)
            {
                throw new DuplicateCommandException(commandName, commands.Select(p => p.FullName));
            }
            return commands.FirstOrDefault();
        }

        public Type GetQuery(string queryName)
        {
            var queries = _queries.Where(p => p.Name.EndsWith(queryName)).ToList();

            if (!queries.Any())
            {
                throw new QueryDoesNotExistException(queryName);
            }
            if (queries.Count() > 1)
            {
                throw new DuplicateQueryException(queryName, queries.Select(p => p.FullName));
            }
            return queries.FirstOrDefault();
        }

        public Type GetQueryHandlerType(IQuery query)
        {
            return _handlers.FindHandlerForQuery(query.GetType());
        }

        public Type GetCommandHandlerType(ICommand command)
        {
            return _handlers.FindHandlerForCommand(command.GetType());
        }

        private IEnumerable<Type> FindTypesImplementingInterface(IEnumerable<Type> allTypes, Type interfaceType)
        {
            return allTypes.Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass);
        }
    }
}