namespace Shops;
using System.Linq;

public class ShopService : IShopService
{
    private readonly List<Shop> _shops = new List<Shop>();
    private readonly List<Buyer> _buyers = new List<Buyer>();
    private readonly List<Product> _products = new List<Product>();
    private int nextId = 100000;

    public string AddBuyer(string buyerName, decimal balance)
    {
        var newBuyer = new Buyer(buyerName, balance);
        _buyers.Add(newBuyer);
        return newBuyer.Name;
    }

    public int AddShop(string shopName, string shopAddress)
    {
        var newShop = new Shop(shopName, shopAddress, nextId++);
        _shops.Add(newShop);
        return newShop.Id;
    }

    public string RegisterProductInShop(int shopId, string productName, decimal productPrice, int productAmount)
    {
        FindShop(shopId)
            .RegisterProductToShop(productName, productPrice, productAmount);
        return productName;
    }

    public void AddProductToBuyerBasket(string buyer, string product, int amount, int shopId)
    {
        GetShop(shopId)
            .AddProductToBuyerBasket(GetBuyer(buyer), product, amount);
    }

    public Shop GetShop(int shopId)
    {
        Shop foundshop = FindShop(shopId);
        if (foundshop is null)
        {
            throw new ShopsException("Foundshop is null");
        }

        return foundshop;
    }

    public Buyer GetBuyer(string buyerName)
    {
        if (string.IsNullOrEmpty(buyerName))
            throw new ShopsException("String is null or empty");
        return _buyers
            .FirstOrDefault(buyer => buyer.Name == buyerName)
               ?? throw new BuyerException("Buyer doesn't found");
    }

    public Product GetProduct(string productName, int shopId)
    {
        if (productName is null)
        {
            throw new ProductException("productName is null");
        }

        if (shopId <= 0)
        {
            throw new ShopsException("shopId is incorrect meaning");
        }

        return GetShop(shopId).GetProduct(productName);
    }

    public Shop FindShop(int id)
    {
        return _shops
            .FirstOrDefault(shop => shop.Id == id);
    }

    public Shop FindMinProduct(string buyer)
    {
        Buyer foundBuyer = GetBuyer(buyer);
        Shop minPriceShop = null;
        decimal minSum = decimal.MaxValue;
        _shops.ForEach(shop =>
        {
            decimal shopSum = foundBuyer.Basket
                .Sum(pair => shop.GetProduct(pair.Key).Price * pair.Value);
            if (shopSum >= minSum) return;
            minSum = shopSum;
            minPriceShop = shop;
        });

        return minPriceShop ?? throw new ProductException("minPriceShop is not valid");
    }

    public void BuyProduct(int shop, string buyer)
    {
        Buyer foundBuyer = GetBuyer(buyer);
        Shop shopFound = FindShop(shop);
        decimal basketPrice = 0;
        foreach ((string productName, int amount) in foundBuyer.Basket)
        {
            basketPrice += shopFound.GetProduct(productName).Price * amount;
        }

        if (foundBuyer.Balance < basketPrice || basketPrice == 0)
        {
            throw new ShopsException("The FondBuyer didn't have enough money or Basket Price = 0");
        }

        foundBuyer.Balance -= basketPrice;
    }

    public void ChangePrice(int shopId, string product, decimal newPrice)
    {
        if (product is null)
        {
            throw new ProductException("product is not valid");
        }

        if (newPrice < 0)
        {
            throw new ProductException("ProductPrice is not valid");
        }

        GetShop(shopId).GetProduct(product).Price = newPrice;
    }
}