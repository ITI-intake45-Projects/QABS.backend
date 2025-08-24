using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QABS.Service;
using QABS.ViewModels.User;
using QABS.ViewModels;
using System.Security.Claims;
using System.Text;

namespace QABS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService accountService;
        public AccountController( AccountService accountService )
        {
            this.accountService = accountService;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterVM user)
        {

            if (ModelState.IsValid)
            {
                var res = await accountService.CreateAccount(user);
                if (res.Succeeded)
                {
                    return new JsonResult(res);
                }

                return new JsonResult(res);

            }

            StringBuilder stringBuilder1 = new StringBuilder();
            foreach (var item in ModelState.Values)
            {
                foreach (var err in item.Errors)
                {
                    stringBuilder1.Append(err.ErrorMessage);
                }
            }
            return new JsonResult(new
            {
                Massage = stringBuilder1.ToString(),
                Status = 400
            });
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginVM vmodel)
        {
            if (ModelState.IsValid)
            {
                var res = await accountService.LoginWithToken(vmodel);
                if (res == null)
                {
                    return new JsonResult(res);
                }
                else if (res == "")
                {
                    return new JsonResult(new
                    {
                        Massage = "Sorry try again Later!!!! Your Accout under Review",
                        Status = 400
                    });
                }
                else
                {

                    var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                    var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                    var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

                    var user = await accountService.GetUserById(userId);

                    return new JsonResult(new
                    {
                        Massage = "Logged in Successfully",
                        Status = 200,
                        Token = res,
                        Role = role,
                        Image = user.ProfileImg,
                        FullName = user.FirstName + " " + user.LastName,
                        Email = email,
                    });

                }
            }

            StringBuilder stringBuilder1 = new StringBuilder();
            foreach (var item in ModelState.Values)
            {
                foreach (var err in item.Errors)
                {
                    stringBuilder1.Append(err.ErrorMessage);
                }
            }

            return new JsonResult(new
            {
                Massage = stringBuilder1.ToString(),
                Status = 400
            });

        }

        [HttpPost("Signout")]
        public async Task<IActionResult> Signout()
        {
            await accountService.Signout();
            return new JsonResult(new
            {
                Massage = "Sign out Successfully",
                Status = 200
            });
        }
    }
}
