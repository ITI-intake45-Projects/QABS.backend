

using LinqKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QABS.Models;
using QABS.Repository;
using QABS.ViewModels;
using QABS.ViewModels.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilities;

namespace QABS.Service
{
    public class AccountService
    {
        private readonly UnitOfWork _unitOfWork;
        IConfiguration appSettingConfiguration;


        public AccountService(UnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            appSettingConfiguration = configuration;
        }

        public async Task<IdentityResult> CreateAccount(UserRegisterVM user)
        {

            if(IsEmailTaken(user.Email).Result)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email is already taken." });
            }

            if(user.ImageFile != null)
            {
                user.ProfileImg = UploadMedia.addimage(user.ImageFile);
            }

            var userRes = await _unitOfWork._userRepository.Register(user);

            if (userRes.Succeeded)
            {
                var currentUser = await _unitOfWork._userRepository.FindByEmailAsync(user.Email);
                if (user.Role == "Admin")
                {
                    //Add Record In Admin table
                    await _unitOfWork._adminRepository.AddAsync(new Admin() { UserId = currentUser.Id });
                    //return IdentityResult.Success;
                }

                else if (user.Role == "Teacher")
                {
                    //Add Record In Teacher table
                    await _unitOfWork._teacherRepository.AddAsync(new Teacher() 
                    { UserId = currentUser.Id, HourlyRate = user.HourlyRate , Specializations = user.Specializations });
                    //return IdentityResult.Success;
                }

                else if (user.Role == "Student")
                {
                    //Add Record In Student table
                    await _unitOfWork._studentRepository.AddAsync(new Student() { UserId = currentUser.Id });
                    //return IdentityResult.Success;
                }
                await _unitOfWork.SaveChangesAsync();
                return IdentityResult.Success;
            }

            return IdentityResult.Failed();
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            var user = await _unitOfWork._userRepository.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<AppUser> GetUserById(string userId)
        {
            return await _unitOfWork._userRepository.FindById(userId);
        }

        //public async Task<SignInResult> Login(UserLoginVM user)
        //{
        //    return await _unitOfWork._userRepository.Login(user);
        //}

        public async Task<string> LoginWithToken(UserLoginVM user)
        {
            var res = await _unitOfWork._userRepository.Login(user);
            if (res.Succeeded)
            {

                //give me data to be encrpted in token
                List<Claim> claims = new List<Claim>();

                var currentUser = await _unitOfWork._userRepository.FindByEmailAsync(user.UserNameOrEmail);

                var roles = await _unitOfWork._userRepository.GetUserRoles(currentUser);

                claims.Add(new Claim(ClaimTypes.Name, currentUser.UserName));
                claims.Add(new Claim(ClaimTypes.Email, currentUser.Email));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, currentUser.Id));
                roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

                //make token    =>      JWT 

                JwtSecurityToken securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(30), // expiration time
                    signingCredentials: new SigningCredentials(
                        algorithm: SecurityAlgorithms.HmacSha256,
                        key: new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettingConfiguration["JWT:PrivateKey"]))
                    )
                );
                return new JwtSecurityTokenHandler().WriteToken(securityToken);

            }

            else if (res.IsLockedOut || res.IsNotAllowed)
            {
                return string.Empty;
            }

            return null;

        }
        public async Task Signout()
        {
            await _unitOfWork._userRepository.Signout();
        }

    }
}
