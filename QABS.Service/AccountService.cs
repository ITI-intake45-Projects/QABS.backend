

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
using System.Net;
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

        public async Task<ServiceResult> CreateAccount(UserRegisterVM user)
        {
            // ✅ تحقق من الإيميل
            if (await IsEmailTaken(user.Email))
            {
                return ServiceResult.FailureResult("الايميل المدخل تم استخدامه من قبل.", HttpStatusCode.BadRequest);
            }

            // ✅ لو فيه صورة، ارفعها
            if (user.ImageFile != null)
            {
                user.ProfileImg = await UploadMedia.AddImageAsync(user.ImageFile);
            }

            // ✅ أنشئ اليوزر باستخدام Identity
            var userRes = await _unitOfWork._userRepository.Register(user);

            if (userRes.Succeeded)
            {
                var currentUser = await _unitOfWork._userRepository.FindByEmailAsync(user.Email);

                // Add role-specific records
                if (user.Role == "Admin")
                {
                    await _unitOfWork._adminRepository.AddAsync(new Admin() { UserId = currentUser.Id });
                }
                else if (user.Role == "Teacher")
                {
                    await _unitOfWork._teacherRepository.AddAsync(new Teacher()
                    {
                        UserId = currentUser.Id,
                        HourlyRate = user.HourlyRate,
                        Specializations = user.Specializations
                    });
                }
                else if (user.Role == "Student")
                {
                    await _unitOfWork._studentRepository.AddAsync(new Student() { UserId = currentUser.Id });
                }

                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.SuccessResult("Account created successfully.");
            }

            // ✅ لو حصلت Errors من Identity
            var errors = string.Join(", ", userRes.Errors.Select(e => e.Description));
            return ServiceResult.FailureResult(errors, HttpStatusCode.BadRequest);
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

        public async Task DeleteAccount(string id)
        {
            var user = _unitOfWork._userRepository.FindById(id);
            if (user != null)
            {
                await _unitOfWork._userRepository.Delete(user.Result);
                await _unitOfWork.SaveChangesAsync();
            }
        }

    }
}
