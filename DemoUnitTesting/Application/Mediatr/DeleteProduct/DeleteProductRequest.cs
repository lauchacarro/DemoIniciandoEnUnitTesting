using DemoUnitTesting.Domain;

using MediatR;

namespace DemoUnitTesting.Application.Mediatr.DeleteProduct
{
    public record DeleteProductRequest(int Id) : IRequest<Result>;
}
