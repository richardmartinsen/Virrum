namespace Virrum.Data
{
    using System.Data.Entity;

    public static class DatabaseContext
    {
        public static void Initialize<T>(IDatabaseInitializer<T> initializer, bool? force = false) where T : DbContext, new()
        {
            Database.SetInitializer(initializer);
            using (var db = new T())
            {
                db.Database.Initialize(force ?? false);
            }
        }
    }
}