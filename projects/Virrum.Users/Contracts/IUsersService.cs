namespace Virrum.Users.Contracts
{
    using Virrum.Users.Models;
    using System.Collections.Generic;

    using Virrum.Data.Models;

    public interface IUsersService
    {
        UserDto GetUser(int userId);

        IEnumerable<UserDto> GetAllUsers();
    }
}
