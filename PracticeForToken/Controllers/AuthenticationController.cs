using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Eshop.DataAccess.Services;
using Eshop.DomainModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PracticeForToken.Dto;

namespace PracticeForToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Injections
        private readonly IUserServiceContract _userServiceContract;

        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration; 
        #endregion

        public AuthenticationController(IUserServiceContract userServiceContract, IMapper mapper, IConfiguration configuration)
        {
            _userServiceContract = userServiceContract ?? throw new ArgumentNullException(nameof(userServiceContract));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthenticateReqestBody authenticateRequestBody)
        {
            if (!await _userServiceContract.ExistsUserAsync(authenticateRequestBody.UserName, authenticateRequestBody.Password))
            {
                return Unauthorized();
            }

            User? user = await _userServiceContract.GetAsync(authenticateRequestBody.UserName, authenticateRequestBody.Password);
            if (user != null)
            {
                LoginDto loginDto = new LoginDto()
                {

                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    Token = GenerateTokenForUser(user.UserId, user.FirstName)
                };
                return Ok(loginDto);
            }

            return Unauthorized();
        }

        private string GenerateTokenForUser(int userId, string firstName)
        
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.UTF8.GetBytes(_configuration["TokenOption:TokenKey"] ?? string.Empty);
            var tokenTimeOut = _configuration.GetValue<int>("TokenTimeOut");
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId",userId.ToString()),
                    new Claim(ClaimTypes.Name,firstName),
                    new Claim(ClaimTypes.System,"")
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenTimeOut),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                NotBefore = DateTime.UtcNow.AddMinutes(-5)

            };
            var securityToken = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(securityToken);
            //adding some text

        }
        
    }
}
