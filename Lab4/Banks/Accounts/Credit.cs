using Banks.Banks;
using Banks.Exceptions;
using Banks.Users;

namespace Banks.Accounts;

public class Credit : Account
{
    private const int IncorrectNumber = 0;
    private decimal _money;
    public Credit(User user, Bank bank, decimal money, int id)
        : base(user, bank, money, id)
    {
        MinusLimit = Bank.BankParameters.CreditLimitMinus;
        CommissionAfterMinus = Bank.BankParameters.CreditPercentCommissionForUseAfterMinus;
        Period = Bank.BankParameters.CreditPeriod;
        PercentAfterRefund = Bank.BankParameters.CreditRefundPercentAfterDayOfRefund;
        _money = money;
        DateCurrentTime = DateTime.Today;
    }

    public int MinusLimit { get; internal set; }
    public decimal CommissionAfterMinus { get; internal set; }
    public int Period { get; internal set; }
    public decimal PercentAfterRefund { get; internal set; }
    public DateTime DateCurrentTime { get; }

    public override void WithdrawalMoney(decimal money)
    {
        // if (User.TrustedUnstrusted == false) throw new BankExceptions("Bank doesn't want to work with a untrusted user");
        if (money <= IncorrectNumber) throw new BankExceptions("Incorrect number of the money");
        if (Money <= IncorrectNumber)
        {
            decimal commission = Money / 100 * CommissionAfterMinus;
            if (Money - money - commission < MinusLimit)
            {
                throw new BankExceptions("Minus limit exceeded");
            }

            Money = Money - money - commission;
        }
        else
        {
            if (Money - money < MinusLimit)
            {
                throw new BankExceptions("Minus Limit exceeded");
            }

            Money -= money;
        }
    }

    public override void RefillMoney(decimal money)
    {
        // if (User.TrustedUnstrustad == false) throw new BankExceptions("Bank doesn't want to work with a untrusted user");
        if (money <= 0) throw new BankExceptions("Incorrect number of the money");
        Money += money;
    }

    public override void UpdateDays(int days, DateTime currDate)
    {
        DateTime dateTime = DateCurrentTime.AddDays(days);
        if (Money < 0)
        {
            CommissionAfterMinus += _money;
        }
    }

    public void ChangeMinusLimit(int newMinusLimit)
    {
        if (newMinusLimit <= IncorrectNumber) throw new BankExceptions("Limit cannot be < 0");
        MinusLimit = newMinusLimit;
    }

    public void ChangeCommissionAfterMinus(decimal newCommission)
    {
        if (newCommission <= IncorrectNumber) throw new BankExceptions("Commission cannot be < 0");
        CommissionAfterMinus = newCommission;
    }

    public void ChangePeriod(int newPeriod)
    {
        if (newPeriod <= IncorrectNumber) throw new BankExceptions("Period cannot be < 0");
        Period = newPeriod;
    }

    public void ChangePercentAfterRefund(decimal newPercent)
    {
        if (newPercent <= IncorrectNumber) throw new BankExceptions("Percent must be > 0");
        PercentAfterRefund = newPercent;
    }
}