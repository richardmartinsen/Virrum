namespace Virrum.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using Virrum.Users.Models;
    using System;

    using Data.Contracts;
    using Data.Extensions;

    using Virrum.Data.Models;
    using Virrum.Users.Contracts;

    public class UsersService : IUsersService
    {
        //  private readonly ISystemTime _systemTime;

        private readonly IVirrumDbProvider _provider;

        //  public UsersService(IVirrumDbProvider provider, ISystemTime systemTime)
        public UsersService(IVirrumDbProvider provider)
        {
            //_systemTime = systemTime;
            _provider = provider;
        }

        public UserDto GetUser(int userId)
        {
            using (var db = _provider.CreateContext())
            {
                return CreateUserDto(db.Users.Find(userId));
            }
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            using (var db = _provider.CreateContext())
            {
                return db.Users.Select(CreateUserDto).ToList();
            }
        }

        private UserDto CreateUserDto(User user)
        {
            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name
            };
            return userDto;
        }
    }
}
