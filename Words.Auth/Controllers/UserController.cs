using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Words.Auth.Domain.Persistence.Models;
using Words.Auth.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Words.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserManager<GameUser> userManager) : Controller
    {
        //private readonly 

        // POST api/<UserController>
        [HttpPost]
        public async Task Post([FromBody] CreateNewUserRequest model)
        {
            var identity = new GameUser
            {
                UserName = $"{model.FirstName}{model.LastName}",
                Email = model.Email,
                LastName = model.LastName,
                FirstName = model.FirstName,
                NickName = model.NickName,
                CountryCode = model.CountryCode,
                DoB = model.DoB
            };
            //userManager.GetRolesAsync()
            var hasher = new PasswordHasher<GameUser>();
            string hashedPassword = hasher.HashPassword(identity, model.Password);
            identity.PasswordHash = hashedPassword;

            await userManager.CreateAsync(identity);
        }
    }
}
