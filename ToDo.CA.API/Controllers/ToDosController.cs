using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.CA.Core.Dtos;
using ToDo.CA.Core.Dtos.Requests;

namespace ToDo.CA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly IMediator mediator;

        public ToDosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateToDoAsync([FromBody]CreateToDoRequest createToDoRequest)
        {
            BaseResponseDto<bool> createResponse = await mediator.Send(createToDoRequest);

            if (createResponse.Data)
            {
                return Created("", createResponse.Data);
            }
            else
            {
                return BadRequest(createResponse.Errors);
            }
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<ToDoDto>>> GetActiveToDosAsync()
        {
            BaseResponseDto<List<ToDoDto>> getActiveToDosReponse = await mediator.Send(new GetActiveToDosRequest());

            if (!getActiveToDosReponse.HasError)
            {
                return Ok(getActiveToDosReponse.Data);
            }
            else
            {
                return BadRequest(getActiveToDosReponse.Errors);
            }
        }
    }
}