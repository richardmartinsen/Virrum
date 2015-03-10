using System;

namespace Qvc.Exception
{
    internal class FactoryFailedToBuildCommandHandler : System.Exception
    {
        public FactoryFailedToBuildCommandHandler(string name) : base("Factory failed to build handler for command " + name)
        {
            
        }
    }
}