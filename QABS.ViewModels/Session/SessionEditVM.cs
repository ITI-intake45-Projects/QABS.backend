

using QABS.Models;

namespace QABS.ViewModels
{
    public class SessionEditVM
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }

        public SessionStatus? SessionStatus { get; set; }
    }
}
