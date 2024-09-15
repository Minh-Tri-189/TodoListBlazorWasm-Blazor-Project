using Microsoft.EntityFrameworkCore;
using TodoList.API.Data;
using TodoList.API.Entites;
using TodoList.Model;

namespace TodoList.API.Respository
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoListDbContext _context;

        public UserRepository(TodoListDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetUserList()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
