using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using Repository.DBModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public User GetUser()
        {
           return  new User
            {
                Name = "Harishanakar",
                UserRole = new UserRole
                {
                    Name = "Admin"
                }
            };
        }

        public  User ValidateUser(User user)
        {

           var res=   from usr in _dbContext.Users
            from rol in _dbContext.UserRoles
            where usr.RoleId == rol.Id && usr.UserName == user.UserName && usr.Password == user.Password
            select new User
            {
                UserId = usr.UserId,
                UserRole = new UserRole
                {
                    Id = rol.Id,
                    Name = rol.Name
                }
            };
            return res.FirstOrDefault(); ;
            
        }

        public async Task<User> CreateUser(User user)
        {
            var us = await _dbContext.Users.FirstOrDefaultAsync(c => c.UserName == user.UserName);
            if (await _dbContext.Users.FirstOrDefaultAsync(c => c.UserName == user.UserName) == null)
            {
                var roleDetails = await _dbContext.UserRoles.FirstOrDefaultAsync(c => c.Name == user.UserRole.Name);
                if (roleDetails != null)
                {
                    var usr = new User
                    {
                        UserName = user.UserName,
                        RoleId = roleDetails.Id,
                        Name = user.Name,
                        Password = user.Password
                    };
                    await _dbContext.Users.AddAsync(usr);
                    var res = await _dbContext.SaveChangesAsync();
                    if (res > 0)
                    {
                        return usr;
                    }
                }
                return null;
            }
            throw new Exception("UserName Already Exist");
        }
    }
}
