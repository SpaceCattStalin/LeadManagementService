using Domain.Common;

namespace Domain.Entities
{
    public class BookingHistory : BaseEntity<Guid>
    {
        public string Content { get; set; } = default!;
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = default!;
    }
}
