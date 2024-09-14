using Microsoft.EntityFrameworkCore;
using TodoList.API.Data;
using TodoList.Api.Repositories;
using Task = TodoList.API.Entites.Task;

namespace TodoList.API.Respository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoListDbContext _context;
        public TaskRepository(TodoListDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Task>> GetTaskList()
        {

            return await _context.Tasks.ToListAsync();
        }
        public async Task<Task> Create(Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> Update(Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> Delete(Task task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> GetById(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        
    }
}
