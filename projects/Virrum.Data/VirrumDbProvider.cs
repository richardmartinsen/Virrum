namespace Virrum.Data
{
    using Virrum.Data.Contracts;

    public class VirrumDbProvider : IVirrumDbProvider
    {
        public IVirrumContext CreateContext()
        {
            var con = new VirrumContext();
            con.Configuration.ProxyCreationEnabled = false;
            return con;
            //return new VirrumContext();
        }

    }
}
