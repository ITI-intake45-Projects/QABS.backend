
using QABS.Models;

namespace QABS.ViewModels
{
    public static class UserExt
    {
        public static AppUser ToModel(this UserRegisterVM viewmodel)
        {
            return new AppUser
            {
                FirstName = viewmodel.FirstName,
                LastName = viewmodel.LastName,
                Gender = viewmodel.Gender,
                Age = viewmodel.Age,
                ProfileImg = viewmodel.ProfileImg,


            };
        }
    }
}
