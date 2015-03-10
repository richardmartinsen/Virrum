using System;

using Newtonsoft.Json;

using Qvc.Exception;
using Qvc.Executable;
using Qvc.Handler;
using Qvc.Repository;
using Qvc.Result;
using Qvc.Validation;
using Qvc.Validation.Metadata;

namespace Qvc
{
    public class JsonEndpoint
    {
        private readonly TypeRepository _typeRepository;

        private readonly Endpoint _endpoint;

        private readonly JsonSerializerSettings _serializerSettings;

        private readonly Action<string, System.Exception> _logErrorAction;

        public JsonEndpoint(IHandlerFactory handlerFactory, TypeRepository typeRepository, JsonSerializerSettings serializerSettings, Action<string, System.Exception> logErrorAction)
        {
            _typeRepository = typeRepository;
            _serializerSettings = serializerSettings;
            _logErrorAction = logErrorAction;
            _endpoint = new Endpoint(handlerFactory, typeRepository, logErrorAction);
        }

        public string Constraints(string name)
        {
            return Stringify(GetConstraints(name));
        }

        public string Command(string name, string json)
        {
            return Stringify(ExecuteCommand(name, json));
        }

        public string Query(string name, string json)
        {
            return Stringify(ExecuteQuery(name, json));
        }

        public ValidationConstraints GetConstraints(string name)
        {
            try
            {
                var executable = ExecutableFromName(name);
                return _endpoint.Constraints(executable);
            }
            catch (System.Exception exception)
            {
                return new ValidationConstraints(exception);
            }
        }

        public CommandResult ExecuteCommand(string name, string json)
        {
            try
            {
                var command = CommandFromJson(name, json);
                return _endpoint.Command(command);
            }
            catch (JsonReaderException exception)
            {
                _logErrorAction(string.Format("Command exception: {0}", name), exception);
                return new CommandResult(new ValidationResult(exception.Message));
            }
            catch (System.Exception exception)
            {
                _logErrorAction(string.Format("Command exception: {0}", name), exception);
                return new CommandResult(exception);
            }
        }

        public QueryResult ExecuteQuery(string name, string json)
        {
            try
            {
                var query = QueryFromJson(name, json);
                return _endpoint.Query(query);
            }
            catch (JsonReaderException exception)
            {
                _logErrorAction(string.Format("Query exception: {0}", name), exception);
                return new QueryResult(new ValidationResult(exception.Message));
            }
            catch (System.Exception exception)
            {
                _logErrorAction(string.Format("Query exception: {0}", name), exception);
                return new QueryResult(exception);
            }
        }

        private string Stringify(object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.None, _serializerSettings);
        }

        private ICommand CommandFromJson(string name, string json)
        {
            var command = _typeRepository.GetCommand(name);
            return (ICommand)JsonConvert.DeserializeObject(json, command, _serializerSettings);
        }

        private IQuery QueryFromJson(string name, string json)
        {
            var query = _typeRepository.GetQuery(name);
            return (IQuery)JsonConvert.DeserializeObject(json, query, _serializerSettings);
        }

        private Type ExecutableFromName(string name)
        {
            try
            {
                return _typeRepository.GetCommand(name);
            }
            catch (CommandDoesNotExistException)
            {
                try
                {
                    return _typeRepository.GetQuery(name);
                }
                catch (QueryDoesNotExistException)
                {
                    throw new ExecutableDoesNotExistException(name);
                }
            }
        }
    }
}