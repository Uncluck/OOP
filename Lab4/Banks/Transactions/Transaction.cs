using Banks.Accounts;
using Banks.Banks;
using Banks.Exceptions;
using Banks.Users;

namespace Banks.Transactions;

public abstract class Transaction
{
    private const int IncorrectNumber = 0;
    protected Transaction(Account userAccount, Account recipient, decimal money, int id)
    {
        UserAccount = userAccount ?? throw new ArgumentNullException(nameof(userAccount));
        Recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
        if (money <= IncorrectNumber) throw new BankExceptions("Money must be > 0");
        Money = money;
        Id = id;
    }

    public Account UserAccount { get; }
    public Account Recipient { get; }
    public decimal Money { get; }
    public int Id { get; }
    public abstract void ExecuteTransaction();
    public abstract void CancelTransaction();
}