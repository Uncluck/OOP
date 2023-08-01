namespace Shops;

public class Buyer
{
    private readonly Dictionary<string, int> _basket;
    public Buyer(string name, decimal balance)
    {
        _basket = new Dictionary<string, int>();
        Balance = balance;
        Name = name;
        if (balance < 0)
        {
            throw new BuyerException("Buyer is not valid");
        }
    }

    public string Name { get; }
    public decimal Balance { get; internal set; }
    internal IReadOnlyDictionary<string, int> Basket => _basket;
    public void AddToBasket(string productName, int amount)
    {
        if (_basket.ContainsKey(productName))
        {
            _basket[productName] += amount;
        }
        else
        {
            _basket[productName] = amount;
        }
    }
}