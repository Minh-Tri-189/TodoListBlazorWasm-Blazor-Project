using Microsoft.EntityFrameworkCore;
using TodoList.API.Data;
using TodoList.Api.Repositories;
using Task = TodoList.API.Entites.Task;
using TodoList.Model;
using TodoList.Model.SeedWork;

namespace TodoList.API.Respository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoListDbContext _context;
        public TaskRepository(TodoListDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Task>> GetTaskList(TaskListSearch taskListSearch)
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

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(x => x.CreateDate)
                .Skip((taskListSearch.PageNumber - 1) * taskListSearch.PageSize)
                .Take(taskListSearch.PageSize)
                .ToListAsync();
            return new PagedList<Task>(data, count, taskListSearch.PageNumber, taskListSearch.PageSize);
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

        public async Task<PagedList<Task>> GetTaskListByUserId(Guid userId, TaskListSearch taskListSearch)
        {
            var query = _context.Tasks
                    .Where(x => x.AssigneeId == userId)
                 .Include(x => x.Assignee).AsQueryable();

            if (!string.IsNullOrEmpty(taskListSearch.NameType))
                query = query.Where(x => x.NameType.Contains(taskListSearch.NameType));

            if (taskListSearch.AssigneeId.HasValue)
                query = query.Where(x => x.AssigneeId == taskListSearch.AssigneeId.Value);

            if (taskListSearch.Priority.HasValue)
                query = query.Where(x => x.Priority == taskListSearch.Priority.Value);

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(x => x.CreateDate)
                .Skip((taskListSearch.PageNumber - 1) * taskListSearch.PageSize)
                .Take(taskListSearch.PageSize)
                .ToListAsync();
            return new PagedList<Entites.Task>(data, count, taskListSearch.PageNumber, taskListSearch.PageSize);
        }
    }
}
