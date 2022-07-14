using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace AirlineManagement.Utility
{
    public class ApplicationContext:IApplicationContext
    {
        public int UserId { get; }
        public string Token { get; }
        public ApplicationContext(IHttpContextAccessor httpContext)
        {
            var handler = new JwtSecurityTokenHandler();
            var authtoken = httpContext.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (!String.IsNullOrEmpty(authtoken))
            {
                var token = handler.ReadJwtToken(authtoken) as JwtSecurityToken;
                Token = authtoken;
                UserId = Convert.ToInt32(token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId).Value);
            }
        }
    }
}
