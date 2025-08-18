using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QABS.ViewModels
{
    public class TeacherPayoutListVM
    {
        public int Id { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public DateTime PaidAt { get; set; }
        public string? TeacherImage { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
