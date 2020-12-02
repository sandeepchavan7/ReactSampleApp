using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ReactSampleApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IConfiguration _config;

        public AuthenticationController(ILogger<AuthenticationController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpPost]
        public Models.LoginResponse Post(Models.LoginRequest model)
        {
            Models.LoginResponse response = new Models.LoginResponse();
            if (ModelState.IsValid)
            {
                if (model.UserName == "admin" && model.Password == "admin123")
                {
                    response.IsSuccess = true;
                    response.Message = "Login successful.";
                    response.UserId = 1491;
                    response.UserName = model.UserName;
                    response.EmailId = "schavan@plexitech.in";

                    //generate new token for authentication.
                    response.Token = GenerateJsonWebToken(response.UserId.ToString(), response.UserName, response.EmailId);
                }
                else
                {
                    response.Message = "Invalid username/password.";
                }
            }
            else
            {
                response.Message = "Invalid request.";
            }
            return response;
        }

        private string GenerateJsonWebToken(string userId, string userName, string emailId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var randomTokenId = Guid.NewGuid().ToString();

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, userName),
                new Claim(JwtRegisteredClaimNames.Email, emailId),
                new Claim(JwtRegisteredClaimNames.Jti, randomTokenId)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
