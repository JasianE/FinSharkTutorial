using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Users;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/account")] // api routes start with api
    [ApiController] // NEEDS THIS TO BECOME A CONTROLLER
    public class AccountController : ControllerBase
    { // this will handle user registration and access

        private readonly UserManager<AppUser> _userManager; // same as like the dbcontext but for user
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpGet("hello")]
        public IActionResult Test()
        {
            return Ok("Hello");
        }
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDTO registerDTO) // we need to use a dto becasue we need to validate the values. 
        {
            try // server errors happen alot when you use usermanager and create async, cause its going to throw errors for validation like password complexity
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); //this will catch the preestablished required validation of our dto, this is not related to the other part that we had by using the dto
                }

                var appUser = new AppUser
                {
                    UserName = registerDTO.Username,
                    Email = registerDTO.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDTO.Password); // this creates a project with a response that you can use to check if it succeded or not
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User"); // this adds the role of user to the user we created
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDTO
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser) // this is perfect because now, the frontend will have the key that they need to send for future requests.
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception error)
            {
                return StatusCode(500, error);
            }
        }
        [HttpPost("login")]

        public async Task<IActionResult> Login(LoggingBuilderExtensions loginDTO)
        {
            
        }
    }
}