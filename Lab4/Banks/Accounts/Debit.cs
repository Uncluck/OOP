using Banks.Banks;
using Banks.Exceptions;
using Banks.Users;

namespace Banks.Accounts;

public class Debit : Account
{
    private const int IncorrectNumber = 0;
    public Debit(User user, Bank bank, decimal money, int id)
        : base(user, bank, money, id)
    {
        PercentageBalance = Bank.BankParameters.PercentageBalance;
        DateCurrentTime = DateTime.Today;
    }

    public int PercentageBalance { get; internal set; }
    public DateTime DateCurrentTime { get; }
    public override void WithdrawalMoney(decimal money)
    {
        // if (User.TrustedUnstrustad == false) throw new BankExceptions("Bank doesn't want to work with a untrusted user");
        if (money <= IncorrectNumber) throw new BankExceptions("Incorrect number of the money");
        if (money > Money) throw new BankExceptions("Money in the account < then can be withdrawal");
        decimal remainderBalance = Money / 100 * PercentageBalance;
        if (Money - money < remainderBalance)
        {
            throw new BankExceptions("Insufficient funds for withdrawal");
        }

        Money -= money;
    }

    public override void RefillMoney(decimal money)
    {
        // if (User.TrustedUnstrustad == false) throw new BankExceptions("Bank doesn't want to work with a untrusted user");
        if (money <= IncorrectNumber) throw new BankExceptions("Incorrect number of the money");
        Money += money;
    }

    public override void UpdateDays(int days, DateTime currDate)
    {
    }

    public void ChangePercentageBalance(int newPercentageBalance)
    {
        if (newPercentageBalance <= IncorrectNumber) throw new BankExceptions("Percent must be > 0");
        PercentageBalance = newPercentageBalance;
    }
}