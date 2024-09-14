using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = TodoList.API.Entites.Task;

namespace TodoList.Api.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetTaskList();
        Task<Task> Create(Task task);

        Task<Task> Update(Task task);

        Task<Task> Delete(Task task);

        Task<Task> GetById(Guid id);
    }
}