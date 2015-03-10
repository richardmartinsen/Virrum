using System;
using System.Collections.Generic;
using System.Reflection;

using Newtonsoft.Json;

using Qvc.Handler;
using Qvc.Repository;

namespace Qvc
{
    public static class Qvc
    {
        public static JsonEndpoint Start(IHandlerFactory handlerFactory, IEnumerable<Assembly> assemblies, JsonSerializerSettings serializerSettings, Action<string, System.Exception> logErrorAction)
        {
            return new JsonEndpoint(handlerFactory, new TypeRepository(assemblies), serializerSettings, logErrorAction);
        }
    }
}
