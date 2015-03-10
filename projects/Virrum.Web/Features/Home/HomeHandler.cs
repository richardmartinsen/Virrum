namespace Virrum.Web.Features.Home
{
    using System.Web.Mvc;
    //using Virrum.Web.Features.Home.Models;

    using Qvc.Exception;
    using Qvc.Handler;

    using Virrum.Data.Models;
    using Virrum.Home.Contracts;
    using Virrum.Web.Features.Home.Contracts;

    public class HomeHandler : IHandleQuery<GetUser, User>
    {
        private readonly IHomeService _homeService;

        public HomeHandler(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public User Handle(GetUser query)
        {
            return _homeService.GetUser(query.Id);
        }
    }
}