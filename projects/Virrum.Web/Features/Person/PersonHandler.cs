using Virrum.Web.Features.Person.Models;

namespace Virrum.Web.Features.Person
{
    using Virrum.Web.Features.Person.Models;
    using System.Web.Mvc;

    using Qvc.Exception;
    using Qvc.Handler;

    using Virrum.Data.Models;
    using Virrum.Users.Contracts;
    using Virrum.Web.Features.Person.Contracts;

    public class UsersHandler : IHandleQuery<GetPerson, PersonsDto>
    {
        private readonly IUsersService _usersService;

        public UsersHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public PersonsDto Handle(GetPerson query)
        {
            return new PersonsDto(); // _usersService.GetUser(query.Id);
        }
    }
}