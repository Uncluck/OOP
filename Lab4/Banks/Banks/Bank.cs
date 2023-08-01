using Banks.Accounts;
using Banks.Exceptions;
using Banks.Users;

namespace Banks.Banks;

public class Bank
{
    private readonly List<Account> _accounts;
    private readonly List<User> _users;
    public Bank(string name, BankParameters bankParameters)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new BankExceptions("Name of bank isn't valid");
        Name = name;
        BankParameters = bankParameters ?? throw new BankExceptions("BankParameters is null");
        _users = new List<User>();
        _accounts = new List<Account>();
    }

    public event EventHandler<NotificationEventArgs> EventBankParameterChanged;
    public string Name { get; }
    public BankParameters BankParameters { get; }

    public void AddAccountForBank(Account account)
    {
        if (account is null) throw new BankExceptions("Account isn't valid");

        _accounts.Add(account);
    }

    public void AddUserForBank(User user)
    {
        if (user is null) throw new BankExceptions("User isn't valid");

        _users.Add(user);
    }

    public void NotifyObservers(string msg)
    {
        EventBankParameterChanged?.Invoke(this, new NotificationEventArgs(msg));
    }

    public void ChangeWithdrawalLimit(int newWithdrawalLimit)
    {
        BankParameters.ChangeWithdrawalLimit(newWithdrawalLimit);
        NotifyObservers("Changed WithdrawalLimit");
    }

    public void ChangeTransferLimit(int newTransferLimit)
    {
        BankParameters.ChangeTransferLimit(newTransferLimit);
        NotifyObservers("Changed TransferLimit");
    }

    public void ChangeMinDepositInterestRate(decimal newMin)
    {
        BankParameters.ChangeMinDepositInterestRate(newMin);
        NotifyObservers("Changed MinDepositInterestRate");
    }

    public void ChangeMidDepositInterestRate(decimal newMid)
    {
        BankParameters.ChangeMidDepositInterestRate(newMid);
        NotifyObservers("Changed MidDepositInterestRate");
    }

    public void ChangeMaxDepositInterestRate(decimal newMax)
    {
       BankParameters.ChangeMaxDepositInterestRate(newMax);
       NotifyObservers("Changed MaxDepositInterestRate");
    }

    public void ChangeDepositTransferPercent(decimal newPercent)
    {
       BankParameters.ChangeDepositTransferPercent(newPercent);
       NotifyObservers("Changed DepositTransferPercent");
    }

    public void ChangeDepositPeriod(int newPeriod)
    {
        BankParameters.ChangeDepositPeriod(newPeriod);
        NotifyObservers("Change DepositPeriod");
    }

    public void ChangeCreditRefundPercentAfterDayOfRefund(decimal newPercent)
    {
        BankParameters.ChangeCreditRefundPercentAfterDayOfRefund(newPercent);
        NotifyObservers("Change CreditPercentRefund");
    }

    public void ChangeCreditPeriod(int newPeriod)
    {
        BankParameters.ChangeCreditPeriod(newPeriod);
        NotifyObservers("Changed CreditPeriod");
    }

    public void ChangeCreditPercentCommissionForUseAfterMinus(decimal newPercent)
    {
        BankParameters.ChangeCreditPercentCommissionForUseAfterMinus(newPercent);
        NotifyObservers("Change CreditPercentCommission");
    }

    public void ChangeCreditLimitMinus(int newLimit)
    {
       BankParameters.ChangeCreditLimitMinus(newLimit);
       NotifyObservers("Change CreditLimit");
    }

    public void SubscribeEvent(User user)
    {
        user.SubscribeEvent();
    }

    public void UnsubscribeEvent(User user)
    {
        user.UnsubscribeEvent();
    }
}