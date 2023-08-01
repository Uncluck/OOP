namespace Shops;

public class Shop
{
    private const int MaxLenghtAddress = 100;
    private const int IncorrectMeaning = 0;
    private readonly List<Product> _products;

    public Shop(string name, string address, int id)
    {
        Id = id;
        if (address.Length is <= 0 or > MaxLenghtAddress)
        {
            throw new ShopsException("Address of shop isn't valid");
        }

        if (Id <= IncorrectMeaning)
        {
            throw new ShopsException("Shop id isn't valid");
        }

        if (string.IsNullOrEmpty(name))
        {
            throw new ShopsException("name of shop isn't valid");
        }

        ShopName = name;
        ShopAddress = address;
        _products = new List<Product>();
    }

    public IReadOnlyList<Product> Products => _products;
    public int Id { get; }
    public string ShopName { get; }
    private string ShopAddress { get; }
    public void AddProductToBuyerBasket(Buyer buyer, string productName, int amount)
    {
        Product product = GetProduct(productName);
        if (product.Amount < amount)
        {
            throw new ShopsException("not enough product");
        }

        buyer.AddToBasket(productName, amount);
        product.Amount -= amount;
    }

    public void RegisterProductToShop(string name, decimal price, int amount)
    {
        _products.Add(new Product(name, price, amount));
    }

    public Product GetProduct(string productName)
    {
        return FindProduct(productName) ?? throw new ShopsException("product not found");
    }

    public Product FindProduct(string name)
    {
        if (name is null) throw new ShopsException("Product is null");
        return _products
            .FirstOrDefault(product => name == product.Name);
    }
}