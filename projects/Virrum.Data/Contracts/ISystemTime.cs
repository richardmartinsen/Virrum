namespace Virrum.Data.Extensions
{
    using System;

    public interface ISystemTime
    {
        DateTime OsloNow { get; }

        DateTime UtcNow { get; }
    }
}