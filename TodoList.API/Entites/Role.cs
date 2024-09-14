using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TodoList.API.Entites
{
    public class Role: IdentityRole<Guid>
    {
        [MaxLength(250)]
        [Required]

        public string Desciption { get; set; }
    }
}
