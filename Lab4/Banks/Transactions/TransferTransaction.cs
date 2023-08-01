using Banks.Accounts;

namespace Banks.Transactions;

public class TransferTransaction : Transaction
{
    private bool _execute;
    private bool _cancel;
    public TransferTransaction(Account userAccount, Account recipient, decimal money, int id)
        : base(userAccount, recipient, money, id)
    {
        _execute = false;
        _cancel = false;
    }

    public override void ExecuteTransaction()
    {
        if (_execute) return;
        UserAccount.WithdrawalMoney(Money);
        Recipient.RefillMoney(Money);
        _execute = true;
    }

    public override void CancelTransaction()
    {
        if (_cancel) return;
        UserAccount?.CancelTransaction(Money);
        Recipient?.WithdrawalMoney(Money);
        _cancel = true;
    }
}