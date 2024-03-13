using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PruebaTokenApi3.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaTokenApi3.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : Controller
	{
		private IConfiguration _config;
		public LoginController(IConfiguration config)
		{
			_config = config;
		}

		[HttpPost]
		public IActionResult Post([FromBody] LoginRequest loginRequest)
		{
			//your logic for login process
			//If login usrename and password are correct then proceed to generate token

			var authClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, loginRequest.User),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Role, loginRequest.Rol?? "Admin")
			};

			var token = GetToken(authClaims);
			return Ok(new
			{
				type = "Bearer",
				token = new JwtSecurityTokenHandler().WriteToken(token),
				expiration = token.ValidTo
			});

		}
		private JwtSecurityToken GetToken(List<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Issuer"],
				expires: DateTime.Now.AddMinutes(120),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);

			return token;
		}
	}
}
