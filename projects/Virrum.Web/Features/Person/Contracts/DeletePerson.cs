namespace Virrum.Web.Features.Person.Contracts
{
    using System.ComponentModel.DataAnnotations;

    using Qvc.Executable;

    public class DeletePerson : ICommand
    {
        [Required]
        public int Id { get; set; }

    }
}