using TodoList.Model;

namespace TodoListBlazorWasm.Services
{

    
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task Logout();
    }
}
