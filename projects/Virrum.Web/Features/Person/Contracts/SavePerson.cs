namespace Virrum.Web.Features.Person.Contracts
{
    using System.ComponentModel.DataAnnotations;

    using Qvc.Executable;

    public class SavePerson : ICommand
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}