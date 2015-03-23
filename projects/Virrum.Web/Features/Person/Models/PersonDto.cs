namespace Virrum.Web.Features.Person.Models
{
    using System.Collections.Generic;
    using Virrum.Users.Models;
    
    public class PersonsDto
    {
        public IEnumerable<UserDto> Persons { get; set; }
    }
}