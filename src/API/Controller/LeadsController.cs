using Application.Leads.Commands.CreateLead;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [ApiController]
    [Route("lead")]
    public class LeadsController : ControllerBase
    {
        private readonly ISender _sender;

        public LeadsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateLead([FromBody] CreateLeadCommand command)
        {
            var id = await _sender.Send(command);
            return CreatedAtAction(nameof(CreateLead), new { id }, id);
        }
    }
}
