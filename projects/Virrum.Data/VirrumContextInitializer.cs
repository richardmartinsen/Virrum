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
            SeedUser(context);
        }

        public static void SeedUser(IVirrumContext context)
        {
            context.Users.AddRange(
                new Collection<User>
                    {
                        new User
                            {
                                Name = "Navn"
                            }
                    });

            context.SaveChanges();
        }
    }
}