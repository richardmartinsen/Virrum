namespace Virrum.Web.Features.Person
{
    using Virrum.Web.Features.Person.Models;
    using System.Web.Mvc;

    using Qvc.Exception;
    using Qvc.Handler;

    using Virrum.Data.Models;
    using Virrum.Person.Contracts;
    using Virrum.Web.Features.Person.Contracts;

    public class UsersHandler : IHandleQuery<GetPerson, PersonsDto>, IHandleCommand<SavePerson>, IHandleCommand<CreatePerson>, IHandleCommand<DeletePerson>
    {
        private readonly IPersonService _personService;

        public UsersHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public PersonsDto Handle(GetPerson query)
        {
            return new PersonsDto(); // _personService.GetUser(query.Id);
        }

        public void Handle(SavePerson command)
        {
            _personService.SavePerson(command.Id, command.Name);
        }

        public void Handle(CreatePerson command)
        {
            _personService.CreatePerson(command.Name);
        }

        public void Handle(DeletePerson command)
        {
            _personService.DeletePerson(command.Id);
        }
    }
}