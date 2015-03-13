namespace Virrum.Web.Features.Home
{
    using Virrum.Web.Features.Home.Models;
    using System.Linq;
    using System.Web.Mvc;

    using Virrum.Data.Contracts;
    using Virrum.Data.Models;
    using Virrum.Home.Contracts;

    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        //[Route("User/{userId}")]
        public ActionResult Index()
        {
            return View(new UsersDto
            {
                Users = _homeService.GetAllUsers()
            });
        }
    }
}