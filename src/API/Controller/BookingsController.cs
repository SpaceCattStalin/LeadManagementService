using Application.UseCases.Commands.CreateBooking;
using Application.UseCases.Queries.GetAllBookingsQuery;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : CustomControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retrieves a paginated list of all bookings based on the provided filters.
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="userFullName"></param>
        /// <param name="userEmail"></param>
        /// <param name="userPhoneNumber"></param>
        /// <param name="interestedCampus"></param>
        /// <param name="interestedAcademicField"></param>
        /// <param name="interestedSpecialization"></param>
        /// <param name="interestedCourse"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("get-all-bookings")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? userFullName = null,
            [FromQuery] string? userEmail = null,
            [FromQuery] string? userPhoneNumber = null,
            [FromQuery] string? interestedCampus = null,
            [FromQuery] string? interestedAcademicField = null,
            [FromQuery] string? interestedSpecialization = null,
            [FromQuery] string? interestedCourse = null,
            [FromQuery] string? status = null,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10
        )
        {
            var query = new GetAllBookingQuery
            {
                UserFullName = userFullName,
                UserEmail = userEmail,
                UserPhoneNumber = userPhoneNumber,
                InterestedCampus = interestedCampus,
                InterestedAcademicField = interestedAcademicField,
                InterestedSpecialization = interestedSpecialization,
                InterestedCourse = interestedCourse,
                Status = status,
                PageNumber = pageIndex,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return OkResponse(result);
        }


        [HttpPost("create-consult-booking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand command)
        {
            var result = await _mediator.Send(command);

            return OkResponse(result);
        }
    }
}
