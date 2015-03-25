namespace Virrum.Web.Features.Person.Contracts
{
    using System.ComponentModel.DataAnnotations;

    using Qvc.Executable;

    public class CreatePerson : ICommand
    {
        [Required]
        public string Name { get; set; }

    }
}