using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Lead : BaseAuditableEntity<Guid>
    {
        public string LeadName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? IntendedMajor { get; set; }
        public string? Notes { get; set; }
        public LeadStatus Status { get; set; }
        public List<LeadAssignment> Assignments = new List<LeadAssignment>();
        public List<LeadInteraction> Interactions = new List<LeadInteraction>();
    }
}
