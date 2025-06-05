using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class LeadCreatedEvent : BaseEvent
    {
        public LeadCreatedEvent(Booking lead)
        {
            Lead = lead;
        }

        public Booking Lead { get; }
    }
}
