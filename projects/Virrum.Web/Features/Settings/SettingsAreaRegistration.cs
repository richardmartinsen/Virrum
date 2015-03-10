namespace Virrum.Web.Features.Settings
{
    using System.Web.Mvc;

    public class SettingsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Settings"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Settings_default", 
                "Settings/{controller}/{action}/{id}", 
                new { controller = "Settings", action = "Index", id = UrlParameter.Optional });
        }
    }
}