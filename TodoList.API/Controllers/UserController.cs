using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Repositories;
using TodoList.API.Respository;
using TodoList.Model;

namespace TodoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetUserList();
            var assignee = users.Select(x => new AssigneeDto()
            {
              Id = x.Id,
              FullName = x.FirstName + " " + x.LastName,
            });
            return Ok(assignee);
        }
    }
}
