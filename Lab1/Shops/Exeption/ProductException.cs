namespace Shops;

public class ProductException : Exception
{
    public ProductException()
        : base("Error")
    {
    }

    public ProductException(string message)
        : base(message)
    {
    }
}