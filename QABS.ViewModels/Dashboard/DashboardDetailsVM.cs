

namespace QABS.ViewModels
{
    public class DashboardDetailsVM
    {
    
            // KPIs
            public int TotalStudents { get; set; }
            public int TotalTeachers { get; set; }
            public int ActiveEnrollments { get; set; }
            public decimal TotalRevenue { get; set; }
            public decimal TotalTeacherPayout { get; set; }

            // Charts
            public List<EnrollmentTrendVM> EnrollmentTrends { get; set; } = new();
            public List<RevenueVsPayoutVM> RevenueVsPayout { get; set; } = new();
            public List<SessionsByStatusVM> SessionsByStatus { get; set; } = new();
            public List<StudentsPerPlanVM> StudentsPerPlan { get; set; } = new();

            // Latest Data
            public List<StudentLatestVM> LatestStudents { get; set; } = new();
            public List<PaymentLatestVM> LatestPayments { get; set; } = new();
        }

        public class EnrollmentTrendVM
        {
            public DateTime Month { get; set; }
            public int Count { get; set; }
        }

        public class RevenueVsPayoutVM
        {
            public DateTime Month { get; set; }
            public decimal Revenue { get; set; }
            public decimal Payout { get; set; }
        }

        public class SessionsByStatusVM
        {
            public string Status { get; set; }
            public int Count { get; set; }
        }

        public class StudentsPerPlanVM
        {
            public string PlanName { get; set; }
            public int Count { get; set; }
        }

        public class StudentLatestVM
        {
            public string StudentName { get; set; }
            public string StudentImg { get; set; }
            public DateTime JoinedAt { get; set; }
        }

        public class PaymentLatestVM
        {
            public string StudentName { get; set; }
            public string StudentImg { get; set; }
            public decimal Amount { get; set; }
            public DateTime Date { get; set; }
        }
    }


