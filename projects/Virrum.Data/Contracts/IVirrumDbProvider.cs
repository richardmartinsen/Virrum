namespace Virrum.Data.Contracts
{
    public interface IVirrumDbProvider
    {
        IVirrumContext CreateContext();
    }
}
