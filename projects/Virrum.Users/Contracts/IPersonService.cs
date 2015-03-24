namespace Virrum.Person.Contracts
{
    using Virrum.Person.Models;
    using System.Collections.Generic;

    using Virrum.Data.Models;

    public interface IPersonService
    {
        PersonDto GetUser(int userId);

        IEnumerable<PersonDto> GetAllUsers();

        void SavePerson(int personId, string name);
    }
}
