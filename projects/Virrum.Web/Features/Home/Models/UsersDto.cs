namespace Virrum.Web.Features.Home.Models
{
    using System.Collections.Generic;
    using Virrum.Home.Models;
    
    public class UsersDto
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}