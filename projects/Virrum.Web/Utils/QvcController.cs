namespace Virrum.Web.Utils
{
    using System.Web.Mvc;

    using Qvc;

    using Virrum.Web.Utils;

    public sealed class QvcController : JsonController
    {
        private readonly JsonEndpoint _jsonEndpoint;

        public QvcController(JsonEndpoint jsonEndpoint)
        {
            this._jsonEndpoint = jsonEndpoint;
        }

        public JsonResult Constraints(string name)
        {
            return this.Json(this._jsonEndpoint.GetConstraints(name), JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult Query(string name)
        {
            return this.Json(this._jsonEndpoint.ExecuteQuery(name, this.Request.Form.Get("parameters")));
        }

        [ValidateInput(false)]
        public JsonResult Command(string name)
        {
            return this.Json(this._jsonEndpoint.ExecuteCommand(name, this.Request.Form.Get("parameters")));
        }
    }
}