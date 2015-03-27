using System.Collections.Generic;

namespace Virrum.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class JobPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Position { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
    }
}
