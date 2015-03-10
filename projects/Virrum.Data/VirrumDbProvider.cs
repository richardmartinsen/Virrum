namespace Virrum.Data
{
    using Virrum.Data.Contracts;

    public class VirrumDbProvider : IVirrumDbProvider
    {
        public IVirrumContext CreateContext()
        {
            return new VirrumContext();
        }

    }
}
