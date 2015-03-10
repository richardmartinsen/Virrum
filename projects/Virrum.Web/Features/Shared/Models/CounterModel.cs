namespace Virrum.Web.Features.Shared.Models
{
    public class CounterModel
    {
        public CounterModel(string counterVm)
        {
            this.CounterVm = counterVm;
            this.ShowValidationMessage = false;
        }

        public string CounterVm { get; set; }
        
        public bool ShowValidationMessage { get; set; }
    }
}