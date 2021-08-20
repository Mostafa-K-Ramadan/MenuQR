using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    {

        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices
            .GetService<IMediator>();

        protected IActionResult HandleResponse<T>(Response<T> response)
        {   
            //string json = JsonConvert.SerializeObject(response, Formatting.Indented);
            //string output = JsonConvert.SerializeObject(response);
            return StatusCode(response.Code, response);
        }
    }
}