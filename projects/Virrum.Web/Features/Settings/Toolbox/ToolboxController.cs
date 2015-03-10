namespace Virrum.Web.Features.Settings.Toolbox
{
    using System.Web.Mvc;

    public class ToolboxController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult CustomBindings()
        {
            return this.View();
        }
    }
}