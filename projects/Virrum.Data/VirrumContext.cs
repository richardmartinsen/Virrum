namespace Virrum.Data
{
    using System.Data.Entity;

    using Virrum.Data.Contracts;
    using Virrum.Data.Models;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class VirrumContext : DbContext, IVirrumContext
    {
        public VirrumContext() : base("Virrum.Data.VirrumContext")
        {
        }

        public IDbSet<Person> Persons { get; set; }

        public IDbSet<JobPosition> JobPositions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
