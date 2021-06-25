using AuthService.Abstracts;
using AuthService.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _authService;
        private ITokenBuilder _tokenBuilder;


        public UserController(IUserService service, ITokenBuilder builder)
        {
            _authService = service;
            _tokenBuilder = builder;
        }

        [HttpPost("create")]
        public IActionResult Register(UserModel userModel)
        {
            try
            {
                _authService.Register(userModel);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(UserModel userModel)
        {
            try
            {
                _authService.Login(userModel);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            var token = _tokenBuilder.BuildToken(userModel.Email);

            return Ok(token);
        }
    }
}
