
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QABS.Models
{
    public class TeacherPayout
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = default!;

        /// <summary>وقت دفع الدفعة فعليًا</summary>
        public DateTime PaidAt { get; set; }

        /// <summary>المجموع النهائي للدفعة (محسوب: عدد الدروس × مدة الدرس × سعر الساعة)</summary>
        
        //public int NumberOfSessions { get; set; } //= PayoutItems.Count;
        //public decimal TotalAmount { get; set; } // sum(list<payoutItem>)

        /// <summary>العملة المستخدمة</summary>
       
        //public string Currency { get; set; } = AppConstants.DefaultCurrency;


        /// <summary>مرفقات إثبات الدفع (اختياري)</summary>
        public string? ImageUrl { get; set; }
        public ICollection<PayoutItem> PayoutItems { get; set; } = new List<PayoutItem>();
        

    }
}
