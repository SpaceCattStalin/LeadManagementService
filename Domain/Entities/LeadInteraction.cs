using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class LeadInteraction : BaseEntity<Guid>
    {
        public Guid LeadId { get; set; }
        public Guid ApplicantId { get; set; }
        public InteractionBy InteractionBy { get; set; }
        public string? Notes { get; set; }
        public DateTime InteractionDate { get; set; }
        public DateTime? NextFollowUpDate { get; set; }
        public Lead Lead { get; set; } = default!;

    }
}
