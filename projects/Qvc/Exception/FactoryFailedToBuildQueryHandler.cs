using System;

namespace Qvc.Exception
{
    internal class FactoryFailedToBuildQueryHandler : System.Exception
    {
        public FactoryFailedToBuildQueryHandler(string name) : base("Factory failed to build query handler for query " + name)
        {
            
        }
    }
}