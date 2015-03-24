// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="">
//   
// </copyright>
// <summary>
//   The user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Virrum.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The user.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
