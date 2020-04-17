using HDFC.Core.Dtos.Masters;
using HDFC.Core.Interfaces;
using HDFC.Core.Repositories;
using HDFC.Infrastructure.Authentication;
using HDFC.Web.Helpers;
using HDFC.Web.ViewModels.Masters.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HDFC.Web.Api.Masters
{
    public class UsersController : BaseApiController
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public UsersController(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<JwtIssuerOptions> jwtOptions, IJwtFactory jwtFactory, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtOptions = jwtOptions.Value;
            _jwtFactory = jwtFactory;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody]UserRegistrationViewModel input)
        {
            var userIdentity = new User()
            {
                UserName = input.Email,
                Email = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
            };
            var result = await _userManager.CreateAsync(userIdentity, input.Password);

            if (!result.Succeeded)
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            return Ok("Account Created");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]UserDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(input.Email, input.Password);
            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }

            var userId = identity.Claims.Single(c => c.Type == "id").Value;
            var user = await _userManager.FindByIdAsync(userId);

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Count == 0)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new Role() { Name = "Admin" });
                }
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            var userDto = new UserDto()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            userDto.Roles = userRoles.Select(s => new RoleDto()
            {
                Name = s
            }).ToList();
            List<Role> roles = new List<Role>();
            foreach (var item in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(item);
                roles.Add(role);
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, input.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented },
                 userDto);
            return new OkObjectResult(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id.ToString()));
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userManager.Users.SingleAsync(u => u.Id == id);
            //var result = _mapper.Map<User, UserDto>(user);
            return Ok(user.Email);
        }
    }
}
