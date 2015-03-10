namespace Virrum.Web.App_Start
{
    using System.Web.Mvc;

    public class FeatureBasedRazorViewEngine : RazorViewEngine
    {
        public FeatureBasedRazorViewEngine()
        {
            ViewLocationFormats = new[]
            {
                "~/Features/{1}/{0}.cshtml",
                "~/Features/{1}/Views/{0}.cshtml",
                "~/Features/Shared/{0}.cshtml",
                "~/Features/Shared/Views/{0}.cshtml"
            };
            MasterLocationFormats = new[]
            {
                "~/Features/{1}/{0}.cshtml",
                "~/Features/{1}/Views/{0}.cshtml",
                "~/Features/Shared/{0}.cshtml",
                "~/Features/Shared/Views/{0}.cshtml"
            };
            PartialViewLocationFormats = new[]
            {
                "~/Features/{1}/{0}.cshtml",
                "~/Features/{1}/Views/{0}.cshtml",
                "~/Features/Shared/{0}.cshtml",
                "~/Features/Shared/Views/{0}.cshtml"
            };
            AreaMasterLocationFormats = new[] 
            {
                "~/Features/{2}/{1}/{0}.cshtml",
                "~/Features/{2}/{1}/Views/{0}.cshtml",
                "~/Features/{2}/{0}.cshtml",
                "~/Features/{2}/Views/{0}.cshtml"
            };
            AreaViewLocationFormats = new[] 
            {
                "~/Features/{2}/{1}/{0}.cshtml",
                "~/Features/{2}/{1}/Views/{0}.cshtml",
                "~/Features/{2}/{0}.cshtml",
                "~/Features/{2}/Views/{0}.cshtml"
            };

            AreaPartialViewLocationFormats = new[]
            {
                "~/Features/{2}/{1}/{0}.cshtml",
                "~/Features/{2}/{1}/Views/{0}.cshtml",
                "~/Features/{2}/{0}.cshtml",
                "~/Features/{2}/Views/{0}.cshtml"
            };

            FileExtensions = new[] { "cshtml" };
        }
    }
}