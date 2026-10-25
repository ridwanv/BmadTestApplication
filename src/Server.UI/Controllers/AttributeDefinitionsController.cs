using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CleanArchitecture.Blazor.Application.Features.AttributeDefinitions.Commands.AddEdit;

namespace CleanArchitecture.Blazor.Server.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttributeDefinitionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AttributeDefinitionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddEditAttributeDefinitionCommand command)
        {
            var id = await _mediator.Send(command);
            if (id == 0) return BadRequest();
            return Ok(id);
        }
    }
}
