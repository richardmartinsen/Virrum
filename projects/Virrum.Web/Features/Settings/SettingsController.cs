namespace Virrum.Web.Features.Settings
{
    using System.Web.Mvc;

    public class SettingsController : Controller
    {

        public ActionResult Index()
        {
            return this.View();
        }
    }
}