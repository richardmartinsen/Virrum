namespace Virrum.Web.Features.Person.Models
{
    using System.Collections.Generic;
    using Virrum.Person.Models;
    
    public class PersonsListDto
    {
        public IEnumerable<PersonDto> Persons { get; set; }
    }
}