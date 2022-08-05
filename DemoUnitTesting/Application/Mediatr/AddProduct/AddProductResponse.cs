namespace DemoUnitTesting.Application.Mediatr.AddProduct
{
    public record AddProductResponse(int Id, string Name, string? Description, double Price, bool IsActive);
}
