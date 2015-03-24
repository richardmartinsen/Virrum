namespace Virrum.Data.Contracts
{
    using System;
    using System.Data.Entity;
    using Models;
    using Virrum.Data.Models;

    public interface IVirrumContext : IDisposable
    {
        IDbSet<Person> Persons { get; set; }

        int SaveChanges();
    }
}
