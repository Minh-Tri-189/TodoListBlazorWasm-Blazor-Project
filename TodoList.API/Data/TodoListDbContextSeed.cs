using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoList.API.Data;
using TodoList.API.Entites;
using TodoList.API.Enums;
using Task = System.Threading.Tasks.Task;

namespace TodoList.API.Data
{
    public class TodoListDbContextSeed
    {
        public async Task SeedAsync(TodoListDbContext context, ILogger<TodoListDbContextSeed> logger)
        {
            if (!context.Users.Any())
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Le",
                    LastName = "Tri",
                    PhoneNumber = "0363518930",
                    UserName = "Admin"
                };

            }
            if (!context.Tasks.Any())
            {
                context.Tasks.Add(new Entites.Task()
                {
                    Id = Guid.NewGuid(),
                    NameType = "Le minh tri",
                    CreateDate = DateTime.Now,
                    Priority = Priority.High,
                    Status = Status.Open
                });

            }
            await context.SaveChangesAsync();
        }
    }
}
