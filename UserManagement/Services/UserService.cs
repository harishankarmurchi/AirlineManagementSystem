using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Abstraction;
using Repository.DBModels;
using Services.Abstraction;
using Services.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepo,IConfiguration config)
        {
            _userRepo = userRepo;
            _configuration = config;
        }

        public  Response<string> ValidateUserAndGetToken(Login logedinUser)
        {
            var response = new Response<string>();
            try
            {
               
                var user = new User { UserName = logedinUser.UserName, Password = logedinUser.Password };
                user = _userRepo.ValidateUser(user);
                if (user != null)
                {
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Data = GenerateJwtToken(user);
                }
                else
                {
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Message = "Invalid User or User NotFound";
                }
                
            }catch(Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }
            return response;
        }

        private string GenerateJwtToken(User user)
        {
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var cred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                
                new Claim(ClaimTypes.Role,user.UserRole.Name),
                new Claim(JwtRegisteredClaimNames.NameId,user.UserId.ToString())
            };
            var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt:Issuer"],
                  audience: _configuration["Jwt:Audience"],
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: cred
                );

            var encode = new JwtSecurityTokenHandler().WriteToken(token);
            return encode;
        }

        public async Task<Response<string>> CreateUser(UserModel userModel)
        {
            var response = new Response<string>();
            try
            {
                var usr=new User
                {
                    UserName = userModel.UserName,
                    Password = userModel.Password,
                    Name=userModel.Name,
                    UserRole= new UserRole { Name=userModel.Role}
                };
               usr=await _userRepo.CreateUser(usr);
                if (usr != null)
                {
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Data = GenerateJwtToken(usr);
                }
                else
                {
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Message = "Unable to create User";
                }

            }
            catch(Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }
            return response;
        }


    }
}
