namespace Virrum.Web.Features.Users
{
    using Virrum.Web.Features.Users.Models;
    using System.Linq;
    using System.Web.Mvc;

    using Virrum.Data.Contracts;
    using Virrum.Data.Models;
    using Virrum.Users.Contracts;

    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        ////[Route("User/{userId}")]
        //public ActionResult Index()
        //{
        //    return RedirectToAction("PersonList");
        //}

        //public ActionResult PersonList()
        //{
        //    return View(new UsersDto
        //    {
        //        Users = _usersService.GetAllUsers()
        //    });
        //}

        //[Route("PersonDetails/{userId}")]
        //public ActionResult PersonDetails(int userId)
        //{
        //    return View("Views/PersonDetails", _usersService.GetUser(userId));
        //}
    }
}