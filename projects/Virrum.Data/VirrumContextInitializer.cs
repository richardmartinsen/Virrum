using System.Data.Entity.Core.Metadata.Edm;

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
            SeedJobPositions(context);
            SeedPerson(context);
        }

        public static void SeedPerson(IVirrumContext context)
        {
            var systemkonsulent = context.JobPositions.Find(1);
            var seniorkonsulent = context.JobPositions.Find(2);
            var radgiver = context.JobPositions.Find(3);

            context.Persons.AddRange(
                new Collection<Person>
                    {
                        new Person
                            {
                                Name = "Fornavn Etternavn",
                                JobPositions = new Collection<JobPosition> {systemkonsulent, seniorkonsulent}
                            },
                        new Person
                            {
                                Name = "Konsulent nr 2",
                                JobPositions = new Collection<JobPosition> {radgiver}
                            }
                    });

            context.SaveChanges();
        }

        public static void SeedJobPositions(IVirrumContext context)
        {
            context.JobPositions.AddRange(
                new Collection<JobPosition>
                    {
                        new JobPosition { Position = "Systemkonsulent" },
                        new JobPosition { Position = "Seniorkonsulent" },
                        new JobPosition { Position = "Rådgiver" },
                        new JobPosition { Position = "Seniorrådgiver" },
                        new JobPosition { Position = "Prosjektleder" },
                        new JobPosition { Position = "Tester" }
                    });

            context.SaveChanges();
        }
    }
}