using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsCapaMedikalAPI.Models;
using CmsCapaMedikalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CmsCapaMedikalAPI.Services.UserService;

namespace CmsCapaMedikalAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel userParam)
        {
            var user = _userService.Authenticate(userParam.UserName, userParam.Password);
            if (user == null)
                return BadRequest(new { message = "Kullanıcı adı veya şifre hatalı!" });
            return Ok(user);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
