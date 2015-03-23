namespace Virrum.Web.Features.Users.Contracts
{
    using System.ComponentModel.DataAnnotations;

    using Qvc.Executable;

    public class GetUser : IQuery
    {
        [Required]
        public int Id { get; set; }
    }
}