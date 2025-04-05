using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaParqueadero.DTOs.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaParqueadero.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration) 
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto model) 
        {
            var user = new IdentityUser {UserName = model.Nombre, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) 
            {
                return BadRequest(new { message = "Error al crear el usuario", result.Errors });
            }
            return Ok(new { message = "Usuario creado correctamente"});
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto model) 
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) 
            {
                return Unauthorized(new { message = "Credenciales Invalidas" });
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
            if (!result.Succeeded) 
            {
                return Unauthorized(new { message = "Credenciales Invalidas" });
            }

            var token = GenerateJwtToken(user);
            return Ok(new AuthResponseDto { Token = token, Mensaje = "Login Exitoso"});
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
