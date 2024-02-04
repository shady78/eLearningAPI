using eLearningAPI.DTO;
using eLearningAPI.Models;
using eLearningAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eLearningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> usermanager;
        public AuthController(IAuthService authService, UserManager<ApplicationUser> usermanager)
        {
            this.usermanager = usermanager;
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.RegisterAsync(model);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
            //return some of the data 
            //return Ok(new {token = result.Token , expireOn = result.ExpiresOn});
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.GetTokenAsync(model);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.AddRoleAsync(model);
            if (!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }
            return Ok(model);
        }
        [HttpPost("send_reset_code")]
        public async Task<IActionResult> SendResetCode(SendResetPassDto model, [FromServices] IEmailProvider _emailProvider)
        {
            if (!ModelState.IsValid) return BadRequest();
            var user = await usermanager.FindByEmailAsync(model.Email);
            if (user is null) return BadRequest("Email Not Found!");
            int pin = await _emailProvider.SendResetCode(model.Email);
            user.PasswordResetPin = pin;
            user.ResetExpires = DateTime.Now.AddMinutes(5);
            await usermanager.UpdateAsync(user);
            return Ok(new
            {
                ExpireAt = user.ResetExpires,
            });
        }
        [HttpPost("reset_code")]
        public async Task<IActionResult> SendResetCode(ResetPassDto model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var user = await usermanager.FindByEmailAsync(model.Email);
            if (user is null || user.ResetExpires is null
               || user.ResetExpires < DateTime.Now || user.PasswordResetPin != model.pin)
                return BadRequest("Invalid Token!");

            //await usermanager.ChangePasswordAsync(user, model.Pass);
            var token = await usermanager.GeneratePasswordResetTokenAsync(user);
            var result = await usermanager.ResetPasswordAsync(user, token, model.Password);
            if (result is null) return BadRequest();
            user.ResetExpires = null;
            user.PasswordResetPin = null;
            await usermanager.UpdateAsync(user);
            return Ok();
        }
    }
}
