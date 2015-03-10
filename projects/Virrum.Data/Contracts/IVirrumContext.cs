namespace Virrum.Data.Contracts
{
    using System;
    using System.Data.Entity;
    using Models;
    using Virrum.Data.Models;

    public interface IVirrumContext : IDisposable
    {
        IDbSet<User> Users { get; set; }

        int SaveChanges();
    }
}
