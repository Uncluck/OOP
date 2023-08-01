namespace Shops;

public class BuyerException : Exception
{
    public BuyerException()
        : base("Error")
    {
    }

    public BuyerException(string message)
        : base(message)
    {
    }
}