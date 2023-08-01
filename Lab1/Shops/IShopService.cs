namespace Shops;

public interface IShopService
{
    string AddBuyer(string buyerName, decimal balance);
    int AddShop(string shopName, string shopAddress);
    string RegisterProductInShop(int shopId, string productName, decimal productPrice, int productAmount);
    void AddProductToBuyerBasket(string buyer, string product, int amount, int shopId);
    Buyer GetBuyer(string buyerName);
    Product GetProduct(string productName, int shopId);
    Shop FindShop(int id);
    Shop FindMinProduct(string buyer);
    void BuyProduct(int shop, string buyer);
    void ChangePrice(int shopId, string product, decimal newPrice);
}