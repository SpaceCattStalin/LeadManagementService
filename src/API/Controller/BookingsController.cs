using Application.UseCases.Commands.ClaimBooking;
using Application.UseCases.Commands.CreateBooking;
using Application.UseCases.Queries.GetAllBookingsQuery;
using Domain.Common;
using Domain.Enums;
using Domain.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : CustomControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        public BookingsController(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }
        /// <summary>
        /// Retrieves a paginated list of all bookings based on the provided filters.
        /// </summary>
        /// <param name="userFullName"></param>
        /// <param name="userEmail"></param>
        /// <param name="userPhoneNumber"></param>
        /// <param name="interestedCampus"></param>
        /// <param name="interestedAcademicField"></param>
        /// <param name="interestedSpecialization"></param>
        /// <param name="location"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("get-all-bookings")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? id = null,
            [FromQuery] string? createdByUserId = null,
            [FromQuery] string? userFullName = null,
            [FromQuery] string? userEmail = null,
            [FromQuery] string? userPhoneNumber = null,
            [FromQuery] string? interestedCampus = null,
            [FromQuery] string? interestedAcademicField = null,
            [FromQuery] string? interestedSpecialization = null,
            [FromQuery] string? location = null,
            [FromQuery] BookingStatus? status = null,
            [FromQuery] ContactStatus? contactStatus = null,
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
                Location = location,
                Status = status,
                ContactStatus = contactStatus,
                PageNumber = pageIndex,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return OkResponse(result);
        }


        [HttpPost("create-consult-booking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return OkResponse(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequestResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }

        }
        [HttpPost("claim-consult-booking")]
        public async Task<IActionResult> ClaimBooking([FromBody] ClaimBookingCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return OkResponse(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequestResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }
    }
}
