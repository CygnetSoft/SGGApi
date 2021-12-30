using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SGGApp.Utilities;
using Swashbuckle.AspNetCore.Annotations;

namespace SGGApp.Api.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        /// <summary>
        /// user generate a new token to access all other api's.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [SwaggerResponse(200)]
        [HttpGet("access/token")]
        public IActionResult GetToken(string name, string password)
        {
            try
            {
                ApiResponse<object> apiResponse = new ApiResponse<object>();

                List<Claim> authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, name),
                        new Claim(ClaimTypes.Hash, password),
                        new Claim(ClaimTypes.Role, "Client"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha384));
                var data = new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    Status = "New token generated",
                    StatsCode = HttpStatusCode.OK
                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
