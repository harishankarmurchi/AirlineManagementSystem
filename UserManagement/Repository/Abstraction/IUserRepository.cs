using Repository.DBModels;
using System.Threading.Tasks;

namespace Repository.Abstraction
{
    public interface IUserRepository
    {
        User GetUser();
        User ValidateUser(User user);
        Task<User> CreateUser(User user);
    }
}
