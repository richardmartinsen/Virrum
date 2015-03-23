using Virrum.Users.Models;

namespace Virrum.Web.Features.Users.Models
{
    using System.Collections.Generic;
    using Virrum.Users.Models;
    
    public class UsersDto
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}