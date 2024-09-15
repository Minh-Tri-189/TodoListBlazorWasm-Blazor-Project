using TodoList.Model;

namespace TodoListBlazorWasm.Services
{
    public interface IUserApiClient
    {
        Task<List<AssigneeDto>> GetAssignee();
    }
}
