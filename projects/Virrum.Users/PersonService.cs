namespace Virrum.Person
{
    using System.Collections.Generic;
    using System.Linq;
    using Virrum.Person.Models;
    using System;

    using Data.Contracts;
    using Data.Extensions;

    using Virrum.Data.Models;
    using Virrum.Person.Contracts;

    public class PersonService : IPersonService
    {
        //  private readonly ISystemTime _systemTime;

        private readonly IVirrumDbProvider _provider;

        //  public PersonService(IVirrumDbProvider provider, ISystemTime systemTime)
        public PersonService(IVirrumDbProvider provider)
        {
            //_systemTime = systemTime;
            _provider = provider;
        }

        public PersonDto GetUser(int personId)
        {
            using (var db = _provider.CreateContext())
            {
                return CreatePersonDto(db.Persons.Find(personId));
            }
        }

        public IEnumerable<PersonDto> GetAllUsers()
        {
            using (var db = _provider.CreateContext())
            {
                return db.Persons.Select(CreatePersonDto).ToList();
            }
        }

        public void SavePerson(int personId, string name)
        {
            using (var db = _provider.CreateContext())
            {
                var person = db.Persons.Find(personId);
                if (person == null)
                {
                    return;
                }

                person.Name = name;

                db.SaveChanges();
            }
        }

        public void CreatePerson(string name)
        {
            using (var db = _provider.CreateContext())
            {
                db.Persons.Add(
                    new Person
                    {
                        Name = name
                    }
                    );

                db.SaveChanges();
            }
        }

        public void DeletePerson(int personId)
        {
            using (var db = _provider.CreateContext())
            {
                var person = db.Persons.Find(personId);
                if (person == null)
                {
                    return;
                }

                db.Persons.Remove(person);

                db.SaveChanges();
            }
        }

        private PersonDto CreatePersonDto(Person person)
        {
            var personDto = new PersonDto
            {
                Id = person.Id,
                Name = person.Name
            };
            return personDto;
        }

    }
}
