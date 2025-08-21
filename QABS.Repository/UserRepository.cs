

using Microsoft.AspNetCore.Identity;
using QABS.Infrastructure;
using QABS.Models;
using QABS.ViewModels;
using QABS.ViewModels.User;

namespace QABS.Repository
{
    public class UserRepositor : BaseRepository<AppUser>
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        public UserRepositor(QABSDbContext dbcontext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : base(dbcontext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> Register(UserRegisterVM accountRegister)
        {
            try
            {
                var res = await userManager.CreateAsync(accountRegister.ToCreate(),
               accountRegister.Password);
                if (res.Succeeded)
                {
                    AppUser account = await userManager.FindByEmailAsync(accountRegister.Email);

                    res = await userManager.AddToRoleAsync(account, accountRegister.Role);

                }
                return res;
            }
            catch
            {
                throw;
            }

        }

        public async Task<SignInResult> Login(UserLoginVM accountLogin)
        {
            var User = await userManager.FindByEmailAsync(accountLogin.UserNameOrEmail);

            if (User != null)
            {
                return await signInManager.PasswordSignInAsync(User, accountLogin.Password, true, true);
            }
            else
            {
                return await signInManager.PasswordSignInAsync(accountLogin.UserNameOrEmail, accountLogin.Password, true, true);
            }
        }

        public async Task<AppUser> FindByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<AppUser> FindById(string Id)
        {
            return await userManager.FindByIdAsync(Id);
        }

        public async Task Signout()
        {
            await signInManager.SignOutAsync();
        }




    }
}
