using Banks.Banks;
using Banks.Exceptions;
using Banks.Users;

namespace Banks.Accounts;

public class Deposit : Account
{
    private const int IncorrectNumber = 0;
    private const int DaysInYear = 365;
    private decimal _percent;
    private List<TimeService> _timeServices;
    private decimal _money;
    private decimal _currentPercent;
    public Deposit(User user, Bank bank, decimal money, int id)
        : base(user, bank, money, id)
    {
        MinPercent = Bank.BankParameters.MinDepositInterestRate;
        MidPercent = Bank.BankParameters.MidDepositInterestRate;
        MaxPercent = Bank.BankParameters.MaxDepositInterestRate;
        Period = Bank.BankParameters.DepositPeriod;
        TransferPercent = Bank.BankParameters.DepositTransferPercent;
        DateCurrentTime = DateTime.Today;
        _money = money;
        _timeServices = new List<TimeService>();
    }

    public decimal MinPercent { get; internal set; }
    public decimal MidPercent { get; internal set; }
    public decimal MaxPercent { get; internal set; }
    public int Period { get; internal set; }
    public decimal TransferPercent { get; internal set; }
    public DateTime DateCurrentTime { get; }

    public override void UpdateDays(int days, DateTime currDate)
    {
        SetPercent();
        _percent += Money * _currentPercent / 100 / DaysInYear;
        Console.WriteLine(currDate);
    }

    public void SetPercent()
    {
        if (Money <= 50000)
        {
            _currentPercent = MinPercent;
        }

        if (Money > 50000 && Money <= 100000)
        {
            _currentPercent = MidPercent;
        }

        if (Money > 100000)
        {
            _currentPercent = MaxPercent;
        }
    }

    public override void WithdrawalMoney(decimal money)
    {
        if (User.TrustedUnstrustad) throw new BankExceptions("Bank doesn't want to work with a untrusted user");
        if (money <= IncorrectNumber) throw new BankExceptions("Incorrect number of the money");
        if (money > Money) throw new BankExceptions("Money in the account > then can be withdrawal");
        Money -= money;
    }

    public override void RefillMoney(decimal money)
    {
        if (User.TrustedUnstrustad) throw new BankExceptions("Bank doesn't want to work with a untrusted user");
        if (money <= IncorrectNumber) throw new BankExceptions("Incorrect number of the money");
        Money += money;
    }

    public void ChangePercent(decimal newMinPercent, decimal newMidPercent, decimal newMaxPercent)
    {
        if (newMinPercent < IncorrectNumber && (newMinPercent > newMidPercent || newMinPercent > newMaxPercent))
            throw new BankExceptions("MinDepositInterestRate isn't valid");
        if (newMidPercent < IncorrectNumber && (newMidPercent < newMinPercent || newMidPercent > newMaxPercent))
            throw new BankExceptions("MidDepositInterestRate isn't valid");
        if (newMaxPercent < IncorrectNumber && (newMaxPercent < newMinPercent || newMaxPercent > newMidPercent))
            throw new BankExceptions("MaxDepositInterestRate isn't valid");
        MinPercent = newMinPercent;
        MidPercent = newMidPercent;
        MaxPercent = newMaxPercent;
    }

    public void ChangePeriod(int newPeriod)
    {
        if (newPeriod <= IncorrectNumber) throw new BankExceptions("Period must be > 0");
        Period = newPeriod;
    }

    public void ChangeTransferPercent(decimal newPercent)
    {
        if (newPercent <= IncorrectNumber) throw new BankExceptions("Percent must be > 0");
        TransferPercent = newPercent;
    }
}