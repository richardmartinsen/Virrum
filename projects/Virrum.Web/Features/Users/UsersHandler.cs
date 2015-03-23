namespace Virrum.Web.Features.Users
{
    using Virrum.Web.Features.Users.Models;
    using System.Web.Mvc;

    using Qvc.Exception;
    using Qvc.Handler;

    using Virrum.Data.Models;
    using Virrum.Users.Contracts;
    using Virrum.Web.Features.Users.Contracts;

    public class UsersHandler : IHandleQuery<GetUser, UsersDto>
    {
        private readonly IUsersService _usersService;

        public UsersHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public UsersDto Handle(GetUser query)
        {
            return new UsersDto(); // _usersService.GetUser(query.Id);
        }
    }
}