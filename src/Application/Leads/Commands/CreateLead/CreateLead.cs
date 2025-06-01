using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Leads.Commands.CreateLead
{
    public record CreateLeadCommand : IRequest<Guid>
    {
        public string LeadName { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string? IntendedMajor { get; init; }
        public string? Notes { get; init; }
    }


    public class CreateLeadCommandHandler : IRequestHandler<CreateLeadCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateLeadCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateLeadCommand request, CancellationToken cancellationToken)
        {
            //var lead = new Booking
            //{
            //    Email = request.Email,
            //    IntendedMajor = request.IntendedMajor,
            //    LeadName = request.LeadName,
            //    Notes = request.Notes,
            //};

            //lead.AddDomainEvent(new LeadCreatedEvent(lead));

            //_context.Leads.Add(lead);

            //await _context.SaveChangesAsync(cancellationToken);
            //return lead.Id;
            return new Guid();
        }
    }
}
