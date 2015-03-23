namespace Virrum.Web.Features.Person.Contracts
{
    using System.ComponentModel.DataAnnotations;

    using Qvc.Executable;

    public class GetPerson : IQuery
    {
        [Required]
        public int Id { get; set; }
    }
}