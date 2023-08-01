using Banks.Banks;
using Banks.Exceptions;
using Banks.Users;

namespace Banks.Accounts;

public abstract class Account
{
    private const decimal IncorrectNumber = 0;

    protected Account(User user, Bank bank, decimal money, int id)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        Bank = bank ?? throw new ArgumentNullException(nameof(bank));
        if (money < IncorrectNumber) throw new BankExceptions("User's money isn't valid");
        Money = money;
        Id = id;
    }

    public User User { get; }
    public Bank Bank { get; }
    public int Id { get; }
    public decimal Money { get; internal set; }
    public abstract void WithdrawalMoney(decimal money);
    public abstract void RefillMoney(decimal money);
    public abstract void UpdateDays(int days, DateTime currDate);

    public void CancelTransaction(decimal money)
    {
        Money += money;
    }
}