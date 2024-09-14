using Microsoft.EntityFrameworkCore;
using TodoList.API.Data;
using TodoList.Api.Extensions;
using Microsoft.AspNetCore.Identity;
using TodoList.API.Entites;
using TodoList.Api.Repositories;
using TodoList.API.Respository;

namespace TodoList.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddDbContext<TodoListDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed((Host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            
            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<TodoListDbContext>()
                .AddDefaultTokenProviders();

          
            var app = builder.Build();

           
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TodoListDbContext>();
                var logger = services.GetRequiredService<ILogger<TodoListDbContextSeed>>();
                new TodoListDbContextSeed().SeedAsync(context, logger).Wait();
            }

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}