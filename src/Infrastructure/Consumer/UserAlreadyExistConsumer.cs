//using Application.Common.Interfaces.UnitOfWork;
//using Domain.Entities;
//using MassTransit;
//using SharedContracts.User;

//namespace Infrastructure.Consumer
//{
//    public class UserAlreadyExistConsumer : IConsumer<UserAlreadyExistsEvent>
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        public UserAlreadyExistConsumer(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task Consume(ConsumeContext<UserAlreadyExistsEvent> context)
//        {
//            var repository = _unitOfWork.GetRepository<Booking>();

//            var booking = await repository.FirstOrDefaultAsync(b => b.UserEmail == context.Message.UserEmail);
//            if (booking != null)
//            {
//                booking.CreatedByUserId = context.Message.UserId;
//                await _unitOfWork.SaveAsync();
//            }
//        }
//    }
//}
