using Domain.Enums;

namespace Application.DTOs
{
    public class BookingFilter
    {
        public string? Id { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Campus { get; set; }
        public string? AcademicField { get; set; }
        public string? Specialization { get; set; }
        public string? Location { get; set; }
        public BookingStatus? Status { get; set; }
        public ContactStatus? ContactStatus { get; set; }
    }
}
