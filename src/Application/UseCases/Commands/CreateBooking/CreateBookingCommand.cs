using MediatR;
using Application.DTOs;

namespace Application.UseCases.Commands.CreateBooking
{
    public class CreateBookingCommand : IRequest<Guid>
    {
        public string UserFullName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string UserPhoneNumber { get; set; } = default!;
        public string Reason { get; set; } = default!;
    }
}
