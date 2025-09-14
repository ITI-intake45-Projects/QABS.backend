using Microsoft.AspNetCore.Http;
using QABS.Models;
using System.ComponentModel.DataAnnotations;

namespace QABS.ViewModels
{
    public class UserRegisterVM
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int Age { get; set; }

        public string? ProfileImg { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? ImageFile { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        //[Compare("Password", ErrorMessage = "Passwords do not match.")]
        //public string ConfirmPassword { get; set; }

        [Range(0, 1000, ErrorMessage = "Hourly rate must be between 0 and 1000.")]
        public decimal? HourlyRate { get; set; }
        //public List<TeacherAvailability>? teacherAvailabilities { get; set; }
        public List<TeacherAvailabilityCreateVM>? teacherAvailabilities { get; set; }

        public List<SpecializationType>? Specializations { get; set; }
    }
}
