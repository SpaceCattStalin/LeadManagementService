using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Booking : BaseEntity<Guid>
    {
        public string UserFullName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string UserPhoneNumber { get; set; } = default!;
        public string? InterestedCampus { get; set; }
        public string? InterestedAcademicField { get; set; }
        public string? InterestedSpecialization { get; set; }
        public string? InterestedCourse { get; set; }
        public string Reason { get; set; } = default!;
        public BookingStatus Status { get; set; }

        // Microservice-safe foreign references
        public Guid CreatedByUserId { get; set; }           // External user who made the request
        public Guid? ClaimedByConsultantId { get; set; }    // Consultant who claimed it (optional)

        public ICollection<BookingHistory> BookingHistories { get; set; } = new List<BookingHistory>();
    }
}
