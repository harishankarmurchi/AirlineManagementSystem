using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Services.Models;
using System.Threading.Tasks;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;       
        }

        [HttpPost]
        [Route("login")]
        public Response<string> Login([FromBody] Login loginUser)
        {
            return _userService.ValidateUserAndGetToken(loginUser);
        }

        [HttpPost]
        [Route("createUser")]
        public async Task<Response<string>> CreateUser([FromBody] UserModel userModel)
        {
            return await _userService.CreateUser(userModel);
        }
    }
}
