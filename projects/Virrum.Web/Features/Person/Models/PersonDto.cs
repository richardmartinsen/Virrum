using Virrum.Data.Models;

namespace Virrum.Web.Features.Person.Models
{
    using System.Collections.Generic;
    using Virrum.Person.Models;
    
    public class PersonsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<JobPosition> JobPositions { get; set; }
    }
}