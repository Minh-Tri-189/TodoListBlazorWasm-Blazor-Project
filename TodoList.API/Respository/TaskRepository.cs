using Microsoft.EntityFrameworkCore;
using TodoList.API.Data;
using TodoList.Api.Repositories;
using Task = TodoList.API.Entites.Task;
using TodoList.Model;

namespace TodoList.API.Respository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoListDbContext _context;
        public TaskRepository(TodoListDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Task>> GetTaskList(TaskListSearch taskListSearch)
        {
            // Bắt đầu từ truy vấn cơ bản với Assignee được bao gồm
            var query = _context.Tasks
                .Include(x => x.Assignee).AsQueryable();

            // Kiểm tra nếu NameType không rỗng và không null
            if (!string.IsNullOrEmpty(taskListSearch?.NameType))
                query = query.Where(x => x.NameType.Contains(taskListSearch.NameType));

            // Kiểm tra nếu AssigneeId có giá trị
            if (taskListSearch?.AssigneeId.HasValue == true)
                query = query.Where(x => x.AssigneeId == taskListSearch.AssigneeId.Value);

            // Kiểm tra nếu Priority có giá trị
            if (taskListSearch?.Priority.HasValue == true)
                query = query.Where(x => x.Priority == taskListSearch.Priority.Value);

            // Trả về danh sách các task sau khi lọc
            return await query.OrderByDescending(x=>x.CreateDate).ToListAsync();
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
