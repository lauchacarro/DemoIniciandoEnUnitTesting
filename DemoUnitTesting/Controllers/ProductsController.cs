
using DemoUnitTesting.Application.Mediatr.AddProduct;
using DemoUnitTesting.Application.Mediatr.DeleteProduct;
using DemoUnitTesting.Application.Services;
using DemoUnitTesting.Extensions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace DemoUnitTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductService _service;

        public ProductsController(IMediator mediator, IProductService service)
        {
            _mediator = mediator;
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => await _service.GetByIdAsync(id).ToActionResult();

        [HttpPost]
        public async Task<IActionResult> Create(AddProductRequest request)
            => await _mediator.Send(request).ToActionResult();

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductRequest request)
            => await _mediator.Send(request).ToActionResult();
    }
}
