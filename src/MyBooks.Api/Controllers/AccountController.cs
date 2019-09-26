using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBooks.Dal.Entities;
using MyBooks.Dto.Dtos;

namespace MyBooks.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userToRegister"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserToRegister userToRegister)
        {
            var user = new ApplicationUser
            {
                UserName = userToRegister.UserName,
                Email = userToRegister.Email
            };
            var result = await _userManager.CreateAsync(user, userToRegister.Password);
            if (!string.IsNullOrEmpty(userToRegister.Level))
            {
                await _userManager.AddClaimAsync(
                    user,
                    new Claim(nameof(userToRegister.Level), userToRegister.Level));
            }
            return Ok();
        }
    }
}