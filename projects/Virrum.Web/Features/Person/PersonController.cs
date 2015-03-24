namespace Virrum.Web.Features.Person
{
    using Virrum.Web.Features.Person.Models;
    using System.Linq;
    using System.Web.Mvc;

    using Virrum.Data.Contracts;
    using Virrum.Data.Models;
    using Virrum.Person.Contracts;

    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        //[Route("User/{userId}")]
        public ActionResult Index()
        {
            return RedirectToAction("PersonList");
        }

        public ActionResult PersonList()
        {
            return View(new PersonsListDto
            {
                Persons = _personService.GetAllUsers()
            });
        }

        [Route("PersonDetails/{userId}")]
        public ActionResult PersonDetails(int userId)
        {
            return View("Views/PersonDetails", _personService.GetUser(userId));
        }

        [Route("PersonEdit/{userId}")]
        public ActionResult PersonEdit(int userId)
        {
            return View("Views/PersonEdit", _personService.GetUser(userId));
        }
    }
}