﻿using Virrum.Home.Models;

namespace Virrum.Home.Contracts
{
    using System.Collections.Generic;

    using Virrum.Data.Models;

    public interface IHomeService
    {
        User GetUser(int userId);

        IEnumerable<UserDto> GetAllUsers();
    }
}
