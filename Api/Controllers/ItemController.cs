using Application.Features.Items.Commands.CreateItem;
using Application.Features.Items.Commands.DeleteItem;
using Application.Features.Items.Commands.UpdateItem;
using Application.Features.Items.Queries;
using Application.Features.Items.Queries.GetItemById;
using Common.DTOs.Requests;
using Common.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/items")]
    [ApiController]
    [Authorize]
    public class ItemController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetItemByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromRoute]int id, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new GetItemByIdQuery { Id = id }, cancellation);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<GetItemsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetIAllPaginated([FromQuery] GetItemsWithPaginationQuery query, CancellationToken cancellation)
        {
            var result = await Mediator.Send(query, cancellation);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateItemResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateItemRequest model, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new CreateItemCommand(model), cancellation);

            return Created(Request.Path.ToUriComponent(), result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateItemRequest model, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new UpdateItemCommand(model), cancellation);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new DeleteItemCommand(id), cancellation);

            return Ok(result);
        }

    }

}
