using Application.Common.Interfaces.Repositories;
using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {

        }

        //public async Task<List<Booking>> SearchAsync(string? id, string fullname, string email, string phone, string campus, string acafield, string specialization, string location, BookingStatus? status, ContactStatus? contactStatus)
        public async Task<List<Booking>> SearchAsync(BookingFilter filter)
        {
            var query = _context.Bookings.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Id) && Guid.TryParse(filter.Id, out var guidId))
            {
                query = query.Where(b => b.Id == guidId);
            }

            if (!string.IsNullOrEmpty(filter.Fullname))
            {
                query = query.Where(b => b.UserFullName.Contains(filter.Fullname));
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(b => b.UserEmail.Contains(filter.Email));
            }

            if (!string.IsNullOrEmpty(filter.Phone))
            {
                query = query.Where(b => b.UserPhoneNumber == filter.Phone);
            }

            if (!string.IsNullOrEmpty(filter.Campus))
            {
                query = query.Where(b => b.InterestedCampus == filter.Campus);
            }

            if (!string.IsNullOrEmpty(filter.AcademicField))
            {
                query = query.Where(b => b.InterestedAcademicField == filter.AcademicField);
            }

            if (!string.IsNullOrEmpty(filter.Specialization))
            {
                query = query.Where(b => b.InterestedSpecialization == filter.Specialization);
            }

            if (!string.IsNullOrEmpty(filter.Location))
            {
                query = query.Where(b => b.Location.Contains(filter.Location));
            }

            if (filter.Status.HasValue)
            {
                query = query.Where(b => b.Status == filter.Status.Value);
            }

            if (filter.ContactStatus.HasValue)
            {
                query = query.Where(b => b.ContactStatus == filter.ContactStatus.Value);
            }

            return await query.OrderByDescending(b => b.CreatedAt).ToListAsync();
        }
    }
}
