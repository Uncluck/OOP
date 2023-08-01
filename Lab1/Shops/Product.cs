namespace Shops;

public class Product
{
    private const int IncorrectMeaning = 0;
    private readonly int productId;
    private int nextIdforProduct = 100000;
    private List<Shop> _shop = new List<Shop>();
    public Product(string name, decimal price, int amount)
    {
        productId = nextIdforProduct++;
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ProductException("Name is not valid");
        }

        if (productId <= IncorrectMeaning)
        {
            throw new ProductException("Id must be > 0");
        }

        Price = price;
        Amount = amount;
        Name = name;
    }

    public string Name { get; }
    public decimal Price { get; internal set; }
    public int Amount { get; internal set; }
}