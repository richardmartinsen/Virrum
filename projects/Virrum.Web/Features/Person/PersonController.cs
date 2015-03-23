namespace Virrum.Web.Features.Person
{
    using Virrum.Web.Features.Person.Models;
    using System.Linq;
    using System.Web.Mvc;

    using Virrum.Data.Contracts;
    using Virrum.Data.Models;
    using Virrum.Users.Contracts;

    public class PersonController : Controller
    {
        private readonly IUsersService _usersService;

        public PersonController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        //[Route("User/{userId}")]
        public ActionResult Index()
        {
            return RedirectToAction("PersonList");
        }

        public ActionResult PersonList()
        {
            return View(new PersonsDto
            {
                Persons = _usersService.GetAllUsers()
            });
        }

        [Route("PersonDetails/{userId}")]
        public ActionResult PersonDetails(int userId)
        {
            return View("Views/PersonDetails", _usersService.GetUser(userId));
        }
    }
}