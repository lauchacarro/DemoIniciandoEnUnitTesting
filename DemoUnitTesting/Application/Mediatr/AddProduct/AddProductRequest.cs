using DemoUnitTesting.Domain;

using MediatR;

namespace DemoUnitTesting.Application.Mediatr.AddProduct
{
    public record AddProductRequest(string Name, string? Description, double Price) : IRequest<Result<AddProductResponse>>;
}
