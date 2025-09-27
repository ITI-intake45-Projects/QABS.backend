using QABS.Models;
using QABS.Repository;
using QABS.ViewModels;
using Utilities;

namespace QABS.Service
{
    public class DashboardService
    {
        private readonly UnitOfWork _unitOfWork;

        public DashboardService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<DashboardDetailsVM>> GetDashboardDetailsAsync()
        {
            try
            {
                var dashboard = new DashboardDetailsVM();

                // ---------- KPIs ----------
                var students = await _unitOfWork._studentRepository.GetAllAsync();
                var teachers = await _unitOfWork._teacherRepository.GetAllAsync();
                var enrollments = await _unitOfWork._enrollmentRepository.GetAllAsync();
                var payments = await _unitOfWork._studentPaymentRepository.GetAllAsync();
                var sessions = await _unitOfWork._sessionRepository.GetAllAsync();

                dashboard.TotalStudents = students.Count;
                dashboard.TotalTeachers = teachers.Count;
                dashboard.ActiveEnrollments = enrollments.Count(e => e.Status == EnrollmentStatus.Active);
                dashboard.TotalRevenue = payments.Sum(p => p.Amount);
                dashboard.TotalTeacherPayout = teachers
                .SelectMany(t => t.TeachersPayouts)  // يجيب كل payouts من كل المدرسين
                .Sum(p => p.TotalAmount);
                // Assuming Teacher has PayoutAmount

                // ---------- Enrollment Trends ----------
                foreach (var e in enrollments)
                {
                    var month = new DateTime(e.StartDate.Year, e.StartDate.Month, 1);
                    var existing = dashboard.EnrollmentTrends.FirstOrDefault(t => t.Month == month);

                    if (existing == null)
                        dashboard.EnrollmentTrends.Add(new EnrollmentTrendVM { Month = month, Count = 1 });
                    else
                        existing.Count++;
                }
                dashboard.EnrollmentTrends = dashboard.EnrollmentTrends.OrderBy(t => t.Month).ToList();

                // ---------- Revenue vs Payout ----------
                // ---------- Revenue vs Payout ----------

                // Revenue
                foreach (var p in payments)
                {
                    var month = new DateTime(p.PaymentDate.Year, p.PaymentDate.Month, 1);
                    var existing = dashboard.RevenueVsPayout.FirstOrDefault(r => r.Month == month);

                    if (existing == null)
                    {
                        dashboard.RevenueVsPayout.Add(new RevenueVsPayoutVM
                        {
                            Month = month,
                            Revenue = p.Amount,
                            Payout = 0
                        });
                    }
                    else
                    {
                        existing.Revenue += p.Amount;
                    }
                }

                // Payout
                var payouts = teachers.SelectMany(t => t.TeachersPayouts).ToList();
                foreach (var payout in payouts)
                {
                    var month = new DateTime(payout.PaidAt.Year, payout.PaidAt.Month, 1); // 👈 لازم TeacherPayout يكون فيه تاريخ دفع
                    var existing = dashboard.RevenueVsPayout.FirstOrDefault(r => r.Month == month);

                    if (existing == null)
                    {
                        dashboard.RevenueVsPayout.Add(new RevenueVsPayoutVM
                        {
                            Month = month,
                            Revenue = 0,
                            Payout = payout.TotalAmount
                        });
                    }
                    else
                    {
                        existing.Payout += payout.TotalAmount;
                    }
                }


                // ---------- Sessions by Status ----------
                foreach (var s in sessions)
                {
                    var status = s.Status.ToString();
                    var existing = dashboard.SessionsByStatus.FirstOrDefault(x => x.Status == status);

                    if (existing == null)
                        dashboard.SessionsByStatus.Add(new SessionsByStatusVM { Status = status, Count = 1 });
                    else
                        existing.Count++;
                }

                // ---------- Students per Plan & Specialization ----------
                foreach (var e in enrollments)
                {
                    var planName = e.SubscriptionPlan?.Name ?? "Unknown";
                    var existing = dashboard.StudentsPerPlan.FirstOrDefault(x => x.PlanName == planName);

                    if (existing == null)
                        dashboard.StudentsPerPlan.Add(new StudentsPerPlanVM { PlanName = planName, Count = 1 });
                    else
                        existing.Count++;

                    var Specialization = e.Specialization.ToString() ?? "Unknown";
                    var existingSpec = dashboard.StudentsPerSpecialization.FirstOrDefault(x => x.Specialization == Specialization);

                    if (existingSpec == null)
                        dashboard.StudentsPerSpecialization.Add(new StudentsPerSpecializationVM
                        { Specialization = Specialization, Count = 1 });

                    else
                        existingSpec.Count++;
                }

                // ---------- Students per Specialization ----------
                //foreach (var e in enrollments)
                //{
                //    var Specialization = e.Specialization.ToString() ?? "Unknown";
                //    var existingSpec = dashboard.StudentsPerSpecialization.FirstOrDefault(x => x.Specialization == Specialization);

                //    if (existingSpec == null)
                //        dashboard.StudentsPerSpecialization.Add(new StudentsPerSpecializationVM 
                //        { Specialization = Specialization, Count = 1 });

                //    else
                //        existingSpec.Count++;
                //}


                // ---------- Latest Students ----------
                dashboard.LatestStudents = students
                    .OrderByDescending(s => s.User.DateCreated) // Assuming User has CreatedAt
                    .Take(5)
                    .Select(s => new StudentLatestVM
                    {
                        StudentName =  $"{ s.User.FirstName } {s.User.LastName}",
                        StudentImg = s.User.ProfileImg,
                        JoinedAt = s.User.DateCreated
                    })
                    .ToList();

                // ---------- Latest Payments ----------
                dashboard.LatestPayments = payments
                    .OrderByDescending(p => p.PaymentDate)
                    .Take(5)
                    .Select(p => new PaymentLatestVM
                    {
                        StudentName =  $"{ p.Student.User.FirstName } {p.Student.User.LastName}",
                        StudentImg = p.Student.User.ProfileImg,
                        Amount = p.Amount,
                        Date = p.PaymentDate
                    })
                    .ToList();

                return ServiceResult<DashboardDetailsVM>.SuccessResult(dashboard, "Dashboard loaded successfully");
            }
            catch (Exception ex)
            {
                return ServiceResult<DashboardDetailsVM>.FailureResult("Error: " + ex.Message);
            }
        }
    }
}

