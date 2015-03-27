using System.Collections.Generic;

namespace Virrum.Person.Models
{
    using System.Collections.ObjectModel;
    using Virrum.Data.Models;

    public class PersonDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<JobPosition> JobPositions { get; set; }
    }
}
