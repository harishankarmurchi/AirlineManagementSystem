using Services.Models;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IUserService
    {
         Response<string> ValidateUserAndGetToken(Login logedinUser);
        Task<Response<string>> CreateUser(UserModel userModel);
    }
}
