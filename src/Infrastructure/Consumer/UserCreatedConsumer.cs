using Application.Common.Interfaces.UnitOfWork;
using Domain.Entities;
using MassTransit;
using SharedContracts.User;

namespace Infrastructure.Consumer
{
    public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserCreatedConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            var repository = _unitOfWork.GetRepository<Booking>();

            var booking = await repository.FirstOrDefaultAsync(b => b.UserEmail == context.Message.UserEmail);
            if (booking != null)
            {
                booking.CreatedByUserId = context.Message.UserId;
                await _unitOfWork.SaveAsync();
            }

        }
    }
}
