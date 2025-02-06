using Hackathon28.Pacientes.Dto;
using Hackathon28.Pacientes.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hackathon28.Pacientes.Controllers
{
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Paciente> _userManager;
        private readonly SignInManager<Paciente> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<Paciente> userManager, 
                              SignInManager<Paciente> signInManager, 
                              IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegisterModel requestParameters)
        {
            if (requestParameters == null)
                return BadRequest();


            if (requestParameters.Password != requestParameters.ConfirmPassword)
                return BadRequest("Verifique as senhas digitadas.");


            var novoPaciente = new Paciente
            {
                UserName = requestParameters.Email,
                Email = requestParameters.Email,
                Cpf = requestParameters.Cpf,
                Nome = requestParameters.Nome
            };

            var result = await _userManager.CreateAsync(novoPaciente, requestParameters.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Usuário criado com sucesso.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel requestParameters)
        {
            if (requestParameters == null)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(requestParameters.Email);

            if (user == null)
                return BadRequest("Usuário ou senha inválidos.");

            var result = await _signInManager.PasswordSignInAsync(user, requestParameters.Password, false, false);

            if (!result.Succeeded)
                return BadRequest("Usuário ou senha inválidos.");

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(Paciente paciente)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, paciente.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, paciente.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
