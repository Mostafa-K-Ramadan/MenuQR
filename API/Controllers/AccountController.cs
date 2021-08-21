using System.Threading.Tasks;
using API.DTOs;
using API.Service;
using Application.Core;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly Token _token;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(Token token, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _token = token;
        }

        protected IActionResult HandleResponse<T>(Response<T> response)
        {
            //string json = JsonConvert.SerializeObject(response, Formatting.Indented);
            //string output = JsonConvert.SerializeObject(response);
            return StatusCode(response.Code, response);
        }
        
        [Authorize(Roles = "roles[0,AA", Policy ="AA")]
        [HttpGet]
        public IActionResult GetAccount()
        {
            return HandleResponse(Response<string>.MakeResponse(true, "yes", 200));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null) return Unauthorized("Invalid email");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                return Ok(CreateUserObject(user));
            }

            return Unauthorized("Invalid password");
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                return BadRequest("Invalid Email");
            }
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            {
                return BadRequest("Invalid Email");
            }

            var user = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest("Problem registering user");

            return Ok(CreateUserObject(user));
        }

        private UserDto CreateUserObject(User user) => new UserDto { UserId = user.Id, Token = _token.CreateToken(user) };
    }
}