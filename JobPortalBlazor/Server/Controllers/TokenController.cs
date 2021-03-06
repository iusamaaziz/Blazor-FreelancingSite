using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using JobPortalBlazor.Shared;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JobPortalBlazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TokenController : ControllerBase
	{
		private readonly JobPortalDBContext _context;
		private readonly UserManager<AspNetUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public TokenController(JobPortalDBContext context, UserManager<AspNetUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[Route("/token")]
		[HttpPost]
		public async Task<IActionResult> Create(string username, string password)
		{
			if (await IsValidUsernameAndPassword(username, password))
			{
				return new ObjectResult(await GenerateToken(username));
			}
			else
			{
				return BadRequest();
			}
		}

		private async Task<bool> IsValidUsernameAndPassword(string username, string password)
		{
			var user = await _userManager.FindByEmailAsync(username);
			return await _userManager.CheckPasswordAsync(user, password);
		}

		private async Task<dynamic> GenerateToken(string username)
		{
			string[] applicationRoles = { "Admin", "Client", "Freelancer" };
			foreach (var role in applicationRoles)
			{
				var roleExists = await _roleManager.RoleExistsAsync(role);
				if (!roleExists)
				{
					await _roleManager.CreateAsync(new IdentityRole(role));
				}
			}

			var user = await _userManager.FindByEmailAsync(username);
			var roles = from ur in _context.UserRoles
						join r in _context.Roles on ur.RoleId equals r.Id
						where ur.UserId == user.Id
						select new { ur.UserId, ur.RoleId, r.Name };


			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, username),
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
			};

			var roled = await _userManager.GetRolesAsync(user);
			foreach (var role in roled)
			{
				var roleClaim = new Claim(ClaimTypes.Role, role);
				claims.Add(roleClaim);
			}


			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, username));
			}

			var token = new JwtSecurityToken(
				new JwtHeader(new SigningCredentials(
					new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMySuperSecretSecurityKeyDoNotShare")),
					SecurityAlgorithms.HmacSha256)),
				new JwtPayload(claims)
				);

			var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

			var output = new
			{
				Access_Token = accessToken,
				Username = username,
				Expires_On = DateTime.Now.AddMinutes(10)
			};

			return output;
		}
	}
}
