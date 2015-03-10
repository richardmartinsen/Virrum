namespace Virrum.Web.Features.Home
{
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

        //public ActionResult Index()
        //{
        //    return View("Views/Index", new User { Id = 1 });
        //}

        //[Route("User/{userId}")]
        public ActionResult Index()
        {
            return View();
            //return View(_homeService.GetUser(1));
        }
    }
}