namespace Virrum.Web.Features.Shared.Models
{
    public class DatepickerModel
    {
        public DatepickerModel(string valueBindingName)
        {
            this.ValueBindingName = valueBindingName;
            this.StartDateBindingName = "undefined";
            this.EndDateBindingName = "undefined";
            this.ShowClearButton = false;
            this.ShowTodayButton = false;
        }

        public string ValueBindingName { get; private set; }

        public string StartDateBindingName { get; set; }

        public string EndDateBindingName { get; set; }

        public bool ShowTodayButton { get; set; }

        public bool ShowClearButton { get; set; }
    }
}