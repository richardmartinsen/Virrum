// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VirrumContext.cs" company="dd">
//   
// </copyright>
// <summary>
//   The virrum context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Virrum.Data
{
    using System.Data.Entity;

    using Virrum.Data.Contracts;
    using Virrum.Data.Models;
    using System.Data.Entity.ModelConfiguration.Conventions;

    /// <summary>
    /// The virrum context.
    /// </summary>
    public class VirrumContext : DbContext, IVirrumContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirrumContext"/> class.
        /// </summary>
        public VirrumContext() : base("Virrum.Data.VirrumContext")
        {
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
