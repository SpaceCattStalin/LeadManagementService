using Domain.Common;

namespace Domain.Entities
{
    public class BookingHistory : BaseAuditableEntity<Guid>
    {
        public string Content { get; set; } = default!;
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = default!;
    }
}
