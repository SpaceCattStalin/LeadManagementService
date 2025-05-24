using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class LeadAssignment : BaseAuditableEntity<Guid>
    {
        public Guid LeadId { get; set; }
        public Guid ApplicantId { get; set; }
        public DateTime AssignedAt { get; set; }
        public LeadAssignmentEnum Status { get; set; }
        public Lead Lead { get; set; } = default!;
    }
}
