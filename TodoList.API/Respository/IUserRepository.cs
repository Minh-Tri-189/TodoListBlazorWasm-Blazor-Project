using TodoList.API.Entites;
using TodoList.Model;

namespace TodoList.API.Respository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUserList();
    }
}
