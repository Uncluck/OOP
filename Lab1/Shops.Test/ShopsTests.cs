using Xunit;

namespace Shops.Test;

public class ShopsTests
{
    private readonly ShopService _sut;

    public ShopsTests()
    {
        _sut = new ShopService();
    }

    [Fact]
    public void BuyerMakesBuy_NotEnoughMoney()
    {
        var newBuyer = _sut.AddBuyer("VALERII MELADZE", 100);
        var newShop = _sut.AddShop("К&Б", "ITMO university");
        var newProduct = _sut.RegisterProductInShop(newShop, "Guiness", 300, 12);
        _sut.AddProductToBuyerBasket(newBuyer, newProduct, 2, newShop);
        Assert.Throws<ShopsException>(() =>
        {
            _sut.BuyProduct(newShop, newBuyer);
        });
    }

    [Fact]
    public void AddProductToShop_ChangePriceProductInTheShop()
    {
        var newShop = _sut.AddShop("К&Б", "bolshoi av. 47");
        var newShop2 = _sut.AddShop("Pivo&Vino", "rushat 2");
        var newProduct = _sut.RegisterProductInShop(newShop, "Baltika", 100, 10);
        var newProduct2 = _sut.RegisterProductInShop(newShop2, "Baltika", 100, 10);
        _sut.ChangePrice(newShop, newProduct, 150);
        Product priceOfProductInNewShop = _sut.GetProduct("Baltika", newShop);
        Product priceOfProductInNewShop2 = _sut.GetProduct("Baltika", newShop2);
        Assert.Equal(150, priceOfProductInNewShop.Price);
        Assert.Equal(100, priceOfProductInNewShop2.Price);
    }

    [Fact]
    public void FindShopWithMinPrice_ShopFound()
    {
        var newBuyer = _sut.AddBuyer("VALERII MELADZE", 20000);
        var newShop = _sut.AddShop("Tabak", "Magadanskii ave. 12");
        var newShop1 = _sut.AddShop("Aromatnii Mir", "gde");
        var newShop2 = _sut.AddShop("Pivo&Vino", "Krasnaia Ploshad'");
        var newProduct = _sut.RegisterProductInShop(newShop, "Baltika", 100, 12);
        var newProduct1 = _sut.RegisterProductInShop(newShop1, "Baltika", 20, 3);
        var newProduct2 = _sut.RegisterProductInShop(newShop2, "Baltika", 30, 3);
        _sut.AddProductToBuyerBasket(newBuyer, newProduct, 2, newShop);
        _sut.AddProductToBuyerBasket(newBuyer, newProduct1, 2, newShop1);
        _sut.AddProductToBuyerBasket(newBuyer, newProduct2, 2, newShop2);
        Assert.Equal(_sut.FindShop(newShop1), _sut.FindMinProduct(newBuyer));
    }

    [Fact]
    public void BuyerMakesBuy_NotEnoughAmount()
    {
        var newBuyer = _sut.AddBuyer("Dora", 10000);
        var newShop = _sut.AddShop("Okey", "Nevski av.");
        var newProduct = _sut.RegisterProductInShop(newShop, "Vino", 500, 5);
        Assert.Throws<ShopsException>(() =>
        {
            _sut.AddProductToBuyerBasket(newBuyer, newProduct, 6, newShop);
            _sut.BuyProduct(newShop, newBuyer);
        });
    }
}