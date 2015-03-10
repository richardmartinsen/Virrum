namespace Virrum.Data.Extensions
{
    using System.Collections.Generic;
    using System.Data.Entity;

    public static class DbSetExtensions
    {
        public static void AddRange<T>(this IDbSet<T> db, IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                db.Add(entity);
            }
        }

        public static void RemoveRange<T>(this IDbSet<T> db, IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                db.Remove(entity);
            }
        }
    }
}