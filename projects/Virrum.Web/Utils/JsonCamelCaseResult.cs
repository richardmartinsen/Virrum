namespace Virrum.Web.Utils
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;

    using Newtonsoft.Json;

    public class JsonCamelCaseResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("GET request not allowed");
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/json";

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data == null)
            {
                return;
            }

            response.Write(JsonConvert.SerializeObject(this.Data, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings));
        }
    }
}