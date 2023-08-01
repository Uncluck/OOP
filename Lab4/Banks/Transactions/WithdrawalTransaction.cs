using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Transactions;

public class WithdrawalTransaction : Transaction
{
    private bool _execute;
    private bool _cancel;
    public WithdrawalTransaction(Account userAccount, Account recipient, decimal money, int id)
        : base(userAccount, recipient, money, id)
    {
        _execute = false;
        _cancel = false;
    }

    public override void ExecuteTransaction()
    {
        if (_execute) throw new BankExceptions("Transaction isn't execute");
        UserAccount.WithdrawalMoney(Money);
        _execute = true;
    }

    public override void CancelTransaction()
    {
        if (_cancel) return;
        UserAccount?.CancelTransaction(Money);
        _cancel = true;
    }
}