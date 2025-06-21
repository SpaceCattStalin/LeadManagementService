using Application.Common.Interfaces.Identity;
using Application.Common.Interfaces.UnitOfWork;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Commands.ClaimBooking
{
    public class ClaimBookingCommandHandler : IRequestHandler<ClaimBookingCommand, ResponseBooking>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClaimBookingCommandHandler(ICurrentUser currentUser, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseBooking> Handle(ClaimBookingCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.Role != "Consultant")
            {
                throw new UnauthorizedAccessException("Chỉ có Tư vấn viên mới được nhận lịch tư vấn");
            }
            var bookingRepository = _unitOfWork.GetRepository<Booking>();
            var booking = bookingRepository.FirstOrDefault(b => b.Id == request.BookingId);

            var bookingHistoryRepoistory = _unitOfWork.GetRepository<BookingHistory>();

            var previousStatus = booking.Status;
            booking.ClaimedByConsultantId = _currentUser.UserId;
            booking.Status = BookingStatus.InProgress;
            booking.ClaimedAt = DateTime.UtcNow;

            var bookingHistory = new BookingHistory
            {
                BookingId = booking.Id,
                Content = $"Lịch hẹn được nhận bởi tư vấn viên {_currentUser.UserId}.",
                PreviousValue = previousStatus.ToString(),
                NewValue = booking.Status.ToString(),
                CreatedAt = DateTime.UtcNow,
                PerformedByUserId = _currentUser.UserId,
            };
            bookingHistoryRepoistory.Add(bookingHistory);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<ResponseBooking>(booking);
        }
    }
}