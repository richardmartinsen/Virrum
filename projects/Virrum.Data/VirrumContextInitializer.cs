namespace Virrum.Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.Linq;
    using Contracts;
    using Extensions;
    using Models;

    public static class VirrumContextInitializer
    {
        public static void Seed(IVirrumContext context)
        {
            SeedPerson(context);
        }

        public static void SeedPerson(IVirrumContext context)
        {
            context.Persons.AddRange(
                new Collection<Person>
                    {
                        new Person
                            {
                                Name = "Fornavn Etternavn"
                            }
                    });

            context.SaveChanges();
        }
    }
}