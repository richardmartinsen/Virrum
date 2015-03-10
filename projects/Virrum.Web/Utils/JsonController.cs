namespace Virrum.Web.Utils
{
    using System.Text;
    using System.Web.Mvc;

    public abstract class JsonController : Controller
    {
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding,
            JsonRequestBehavior behavior)
        {
            return new JsonCamelCaseResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}