namespace Shops;

public class ShopsException : Exception
{
    public ShopsException()
        : base("Error")
    {
    }

    public ShopsException(string message)
        : base(message)
    {
    }
}