using Medications.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Medications.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MedicationsContext context;

        public AccountController(MedicationsContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(User loginDto)
        {
            // تحقق من صحة بيانات المستخدم
            var user = context.Users.SingleOrDefault(u => u.Email == loginDto.Email && u.Password == loginDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid login attempt.");
            }

            // توليد JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Your_Secret_Key_Here");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, user.Role) // إضافة الدور (طبيب أو مريض)
                }),
                Expires = DateTime.UtcNow.AddHours(1), // صلاحية التوكن لمدة ساعة
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}