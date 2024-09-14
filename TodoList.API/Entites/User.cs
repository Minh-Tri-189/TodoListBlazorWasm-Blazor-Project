using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TodoList.API.Entites
{
    public class User : IdentityUser<Guid>
    {
        [MaxLength(250)]
        [Required]

        public String FirstName{ get; set; }
        [Required]
        public string LastName{ get; set; }
    }
}
