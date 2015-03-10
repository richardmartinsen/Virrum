namespace Virrum.Home
{
    using System;

    using Data.Contracts;
    using Data.Extensions;

    using Virrum.Data.Models;
    using Virrum.Home.Contracts;

    public class HomeService : IHomeService
    {
        //  private readonly ISystemTime _systemTime;

        private readonly IVirrumDbProvider _provider;

        //  public HomeService(IVirrumDbProvider provider, ISystemTime systemTime)
        public HomeService(IVirrumDbProvider provider)
        {
            //_systemTime = systemTime;
            _provider = provider;
        }

        public User GetUser(int userId)
        {
            using (var db = _provider.CreateContext())
            {
                return db.Users.Find(userId);
            }
        }
    }
}
