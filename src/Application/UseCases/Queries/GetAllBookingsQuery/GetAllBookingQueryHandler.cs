using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Models;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Queries.GetAllBookingsQuery
{
    public class GetAllBookingQueryHandler : IRequestHandler<GetAllBookingQuery, PaginatedList<ResponseBooking>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBookingQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IBookingRepository bookingRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bookingRepository = bookingRepository;
        }
        public async Task<PaginatedList<ResponseBooking>> Handle(GetAllBookingQuery request, CancellationToken cancellationToken)
        {
            var filter = new BookingFilter
            {
                Id = request.Id,
                Fullname = request.UserFullName,
                Email = request.UserEmail,
                Phone = request.UserPhoneNumber,
                Campus = request.InterestedCampus,
                AcademicField = request.InterestedAcademicField,
                Specialization = request.InterestedSpecialization,
                Location = request.Location,
                Status = request.Status,
                ContactStatus = request.ContactStatus
            };

            var allResults = await _bookingRepository.SearchAsync(filter);

            var totalCount = allResults.Count();

            var items = allResults
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var responseList = new List<ResponseBooking>();
            foreach (var item in items)
            {
                var responseBooking = _mapper.Map<ResponseBooking>(item);
                responseList.Add(responseBooking);
            }

            return new PaginatedList<ResponseBooking>(responseList, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
