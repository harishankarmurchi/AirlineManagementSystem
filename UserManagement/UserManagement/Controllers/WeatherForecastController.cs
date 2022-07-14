using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Utility;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IApplicationContext context,IUserRepository repo,IConfiguration config)
        {
            _logger = logger;
            _userRepo = repo;
            _configuration = config;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("getToken")]
        public string GetToken()
        {
            var user = _userRepo.GetUser();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var cred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,"Harish"),
                new Claim(JwtRegisteredClaimNames.Email,"Harish"),
                new Claim(JwtRegisteredClaimNames.Jti,"Harish"),
                new Claim(ClaimTypes.Role,"user"),
                new Claim(JwtRegisteredClaimNames.NameId,"1")
            };
            var token = new JwtSecurityToken(
                  issuer: "UserManagement",
                  audience: "UserManagement",
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: cred
                );

            var encode = new JwtSecurityTokenHandler().WriteToken(token);
            return encode;
        }
    }
}
