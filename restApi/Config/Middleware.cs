using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restApi.Config
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public Middleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (authHeader == null || authHeader.Equals(""))
            {
                context.Response.StatusCode = 401;
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = this._configuration.GetSection("Jwt:Secret").Value;
            var token = authHeader.Split(" ")[1];
            SecurityToken validatedToken;


            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
            {
                context.Response.StatusCode = 401;
                return;
            }

            var validationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
            };
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                if(validatedToken != null)
                {
                    await _next(context);                    
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 401;
                return ;
            }
        }
    }
}
