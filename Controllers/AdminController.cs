using Comp584_Server_Flores.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using WorldModel;

namespace Comp584_Server_Flores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<WorldModelUser> userManager, JwtHandler jwtHandler) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login (LoginRequest loginRequest)
        {
           WorldModelUser? worldUser = await userManager.FindByNameAsync(loginRequest.Username);
            /*  if (worldUser is null || !await userManager.CheckPasswordAsync(worldUser, loginRequest.Password))
              {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }*/
            if (worldUser is null)
            {
                return Unauthorized("Invalid username.");
            }
           bool loginStatus = await userManager.CheckPasswordAsync(worldUser, loginRequest.Password);
              if (!loginStatus)
              {
                 return Unauthorized("Invalid password.");
              }

            JwtSecurityToken jwttoken = await jwtHandler.GenerateTokenAsync(worldUser);
            string stringtoken = new JwtSecurityTokenHandler().WriteToken(jwttoken);
            return Ok(new LoginResponse { 
                success = true,
                Message = "Login successful.",
                token = stringtoken
            });
        }
    }
}
