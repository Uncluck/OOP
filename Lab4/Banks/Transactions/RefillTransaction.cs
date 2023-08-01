using Banks.Accounts;

namespace Banks.Transactions;

public class RefillTransaction : Transaction
{
    private bool _execute;
    private bool _cancel;
    public RefillTransaction(Account userAccount, Account recipient, decimal money, int id)
        : base(userAccount, recipient, money, id)
    {
        _execute = false;
        _cancel = false;
    }

    public override void ExecuteTransaction()
    {
        if (_execute) return;
        UserAccount.RefillMoney(Money);
        _execute = true;
    }

    public override void CancelTransaction()
    {
        if (_cancel) return;
        UserAccount?.WithdrawalMoney(Money);
        _cancel = true;
    }
}