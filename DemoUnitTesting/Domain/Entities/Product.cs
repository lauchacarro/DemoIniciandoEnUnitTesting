namespace DemoUnitTesting.Domain.Entities
{
    public class Product
    {
        public Product(int id, string name, string? description, double price, bool isActive)
            : this(name, description, price, isActive)
        {
            Id = id;
        }

        public Product(string name, string? description, double price, bool isActive)
        {
            Name = name;
            Description = description;
            Price = price;
            IsActive = isActive;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
    }
}
