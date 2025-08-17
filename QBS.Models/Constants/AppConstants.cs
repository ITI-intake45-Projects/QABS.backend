

namespace QABS.Models
{
    public class AppConstants
    {
        // مدة الحصة الافتراضية بالساعات
        public const double DefaultSessionDurationHours = 2;

        // العملة الافتراضية للدفعات
        public const string DefaultCurrency = "جُنَيْهًا ";

        // السعر الافتراضي لكل ساعة (يمكن وضعه كقيمة تقريبية، أو استخدامه كـ fallback)
        public const decimal DefaultHourlyRate = 50m;

        // أي إعدادات عامة أخرى
        public const int MaxSessionsPerEnrollment = 8;
    }
}
