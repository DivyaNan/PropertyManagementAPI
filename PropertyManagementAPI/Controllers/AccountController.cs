using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PropertyManagementAPI.Data;
using PropertyManagementAPI.DTOs;
using PropertyManagementAPI.Interfaces;
using PropertyManagementAPI.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PropertyManagementAPI.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public AccountController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this._unitOfWork = unitOfWork;
            this._configuration = configuration;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDTO loginReq)
        {
            var user = await _unitOfWork.UserRepository.Authenticate(loginReq.UserName, loginReq.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var loginRes = new LoginResponseDTO
            {
                UserName = user.UserName,
                Token = CreateJWT(user),
            };
            return Ok(loginRes);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDTO loginReq)
        {
            if (await _unitOfWork.UserRepository.UserAlreadyExists(loginReq.UserName))
                return BadRequest("User already exists, please try something else");

            _unitOfWork.UserRepository.Register(loginReq.UserName, loginReq.Password);
            await _unitOfWork.SaveAsync();
            return StatusCode(201);

        }
        private string CreateJWT(User user)
        {

            var secretKey = _configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                            secretKey));
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDecsriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = signingCredentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDecsriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
