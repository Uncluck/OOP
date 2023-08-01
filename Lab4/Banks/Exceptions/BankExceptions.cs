namespace Banks.Exceptions;

public class BankExceptions : Exception
{
    public BankExceptions()
        : base("Error")
    {
    }

    public BankExceptions(string message)
        : base(message)
    {
    }
}