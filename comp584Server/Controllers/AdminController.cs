using comp584Server.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IdentityModel.Tokens.Jwt;
using WorldModel;

namespace comp584Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<WorldModelUsers> userManager, JwtHandling handler) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login (loginRequest loginRequest)
        {
            WorldModelUsers? checkeduser = await userManager.FindByNameAsync(loginRequest.UserName);
            if (checkeduser == null) {
               return Unauthorized("Invalid Username");
            }
           bool passwordtest = await userManager.CheckPasswordAsync(checkeduser,loginRequest.Password);
            if (!passwordtest) {
                return Unauthorized("Invalid Password");
            }
            JwtSecurityToken Token = await handler.GenerateToken(checkeduser);
            string string_token = new JwtSecurityTokenHandler().WriteToken(Token);
            return Ok(new LoginResponse
            {
                Sucess = true,
                Message = "Login Success",
                Token = string_token
            });
        }
    }
}
