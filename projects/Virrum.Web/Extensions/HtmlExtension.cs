namespace Virrum.Web.Extensions
{
    using System.Web.Mvc;

    public static class HtmlExtension
    {
        public static MvcHtmlString CurrentHashActionLink(
        this HtmlHelper htmlHelper,
        string linkText,
        string actionName = null,
        string controllerName = null,
        object arg = null)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            actionName = actionName ?? linkText;
            var url = controllerName == null
                          ? (arg == null ? urlHelper.Action(actionName) : urlHelper.Action(actionName, arg))
                          : (arg == null ? urlHelper.Action(actionName, controllerName) : urlHelper.Action(actionName, controllerName, arg));

            var currentAction = htmlHelper.ViewContext.RouteData.Values["action"].ToString();
            var currentController = htmlHelper.ViewContext.RouteData.Values["controller"].ToString();

            var active = string.Compare(currentAction, actionName, true) == 0
                         && (controllerName == null || string.Compare(currentController, controllerName, true) == 0)
                             ? "active"
                             : string.Empty;
            var html = string.Format("<li class=\"{0}\"><a href=\"#{1}\">{2}</a></li>", active, url, linkText);
            return new MvcHtmlString(html);
        }
        
        public static MvcHtmlString CurrentActionLink(
        this HtmlHelper htmlHelper,
        string linkText,
        string actionName = null,
        string controllerName = null,
        object arg = null)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            actionName = actionName ?? linkText;
            var url = controllerName == null
                          ? (arg == null ? urlHelper.Action(actionName) : urlHelper.Action(actionName, arg))
                          : (arg == null ? urlHelper.Action(actionName, controllerName) : urlHelper.Action(actionName, controllerName, arg));

            var currentAction = htmlHelper.ViewContext.RouteData.Values["action"].ToString();
            var currentController = htmlHelper.ViewContext.RouteData.Values["controller"].ToString();

            var active = string.Compare(currentAction, actionName, true) == 0
                         && (controllerName == null || string.Compare(currentController, controllerName, true) == 0)
                             ? "active"
                             : string.Empty;
            var html = string.Format("<li class=\"{0}\"><a href=\"{1}\">{2}</a></li>", active, url, linkText);
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString CurrentControllerLink(
            this HtmlHelper htmlHelper, 
            string linkText, 
            string actionName = null, 
            string controllerName = null, 
            object arg = null)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            actionName = actionName ?? linkText;
            var url = controllerName == null
                          ? (arg == null ? urlHelper.Action(actionName) : urlHelper.Action(actionName, arg))
                          : (arg == null ? urlHelper.Action(actionName, controllerName) : urlHelper.Action(actionName, controllerName, arg));

            var currentController = htmlHelper.ViewContext.RouteData.Values["controller"].ToString();

            var active = (controllerName == null || string.Compare(currentController, controllerName, true) == 0)
                             ? "active"
                             : string.Empty;
            var html = string.Format("<li class=\"{0}\"><a href=\"{1}\">{2}</a></li>", active, url, linkText);
            return new MvcHtmlString(html);
        }
    }
}