using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoList.API.Entites;
using Task = TodoList.API.Entites.Task;

namespace TodoList.API.Data
{
    public class TodoListDbContext : IdentityDbContext<User,Role,Guid>
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
        {

        }
        public DbSet<Task> Tasks { get; set; }
    }
}
