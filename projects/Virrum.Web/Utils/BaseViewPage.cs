namespace Virrum.Web.Utils
{
    using System.Web.Http;
    using System.Web.Mvc;

    using Newtonsoft.Json;

    public abstract class BaseViewPage<T> : WebViewPage<T>
    {
        private readonly JsonSerializerSettings _settings;

        protected BaseViewPage()
        {
            this._settings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
        }

        public string Version
        {
            get { return this.GetType().Assembly.GetHashCode().ToString(); }
        }

        public string Json(object data)
        {
            return JsonConvert.SerializeObject(data, this._settings);
        }

    }
}