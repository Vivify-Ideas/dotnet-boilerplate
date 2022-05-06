using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}