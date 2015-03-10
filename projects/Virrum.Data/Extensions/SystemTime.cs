namespace Virrum.Data.Extensions
{
    using System;

    public class SystemTime : ISystemTime
    {
        public static readonly TimeZoneInfo OsloTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

        public DateTime OsloNow
        {
            get
            {
                return DateTime.SpecifyKind(TimeZoneInfo.ConvertTimeFromUtc(UtcNow, OsloTimeZone), DateTimeKind.Local);
            }
        }

        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }

    }
}