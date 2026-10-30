using Academy_2025.Dtos;
using Academy_2025.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Academy_2025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync([FromBody] LoginDto loginDto)
        {
            var user = await _accountService.LoginAsync(loginDto);
            if (user == null)
            {
                return Unauthorized();
            }

            return _tokenService.CreateToken(user);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> MeAsync()
        {
            var userIdValue =
                User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out var userId))
            {
                return Unauthorized();
            }

            var user = await _accountService.GetCurrentUserAsync(userId);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role,
                Age = user.Age
            });
        }
    }
}
