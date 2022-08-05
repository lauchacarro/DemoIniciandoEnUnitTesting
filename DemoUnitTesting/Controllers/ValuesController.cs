
using AWEcommerce.Api.Infrastructure.Extensions;

using DemoUnitTesting.Application.Mediatr.AddProduct;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace DemoUnitTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Create(AddProductRequest request)
            => await _mediator.Send(request).ToActionResult();
    }
}
