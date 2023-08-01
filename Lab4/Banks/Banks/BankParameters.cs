using Banks.Exceptions;

namespace Banks.Banks;

public class BankParameters
{
    private const int MinPeriod = 100;
    private const int MaxWithdrawalLimit = -150000;
    private const decimal PercentInBank = 20;
    private const decimal IncorrectNumber = 0;
    public BankParameters(
        int withdrawalLimit,
        int transferLimit,
        int percentageBalance,
        decimal minDepositInterestRate,
        decimal midDepositInterestRate,
        decimal maxDepositInterestRate,
        decimal depositTransferPercent,
        int depositPeriod,
        decimal creditRefundPercentAfterDayOfRefund,
        int creditPeriod,
        decimal creditPercentCommissionForUseAfterMinus,
        int creditLimitMinus)
    {
        if (withdrawalLimit > IncorrectNumber && withdrawalLimit < MaxWithdrawalLimit)
            throw new BankExceptions("Limit of the withdrawal isn't valid");
        if (transferLimit < IncorrectNumber && transferLimit > MaxWithdrawalLimit)
            throw new BankExceptions("Limit of the transfer isn't valid");
        if (percentageBalance < IncorrectNumber) throw new BankExceptions("Percentage Balance isn't correct");
        if (minDepositInterestRate < IncorrectNumber && (minDepositInterestRate > midDepositInterestRate || minDepositInterestRate > maxDepositInterestRate))
            throw new BankExceptions("MinDepositInterestRate isn't valid");
        if (midDepositInterestRate < IncorrectNumber && (midDepositInterestRate < minDepositInterestRate || midDepositInterestRate > maxDepositInterestRate))
            throw new BankExceptions("MidDepositInterestRate isn't valid");
        if (maxDepositInterestRate < IncorrectNumber && (maxDepositInterestRate < minDepositInterestRate || maxDepositInterestRate > midDepositInterestRate))
            throw new BankExceptions("MaxDepositInterestRate isn't valid");
        if (depositTransferPercent < IncorrectNumber) throw new BankExceptions("Deposit Transfer Percent isn't valid");
        if (depositPeriod < MinPeriod) throw new BankExceptions("Deposit Period isn't valid");
        if (creditRefundPercentAfterDayOfRefund > PercentInBank) throw new BankExceptions("Credit Refund isn't valid");
        if (creditPeriod < MinPeriod) throw new BankExceptions("Credit Period isn't valid");
        if (creditPercentCommissionForUseAfterMinus > PercentInBank) throw new BankExceptions("Credit Percent isn't valid");
        if (creditLimitMinus < MaxWithdrawalLimit) throw new BankExceptions("Credit limit after limit isn't valid");
        WithdrawalLimit = withdrawalLimit;
        TransferLimit = transferLimit;
        PercentageBalance = percentageBalance;
        MinDepositInterestRate = minDepositInterestRate;
        MidDepositInterestRate = midDepositInterestRate;
        MaxDepositInterestRate = maxDepositInterestRate;
        DepositTransferPercent = depositTransferPercent;
        DepositPeriod = depositPeriod;
        CreditRefundPercentAfterDayOfRefund = creditRefundPercentAfterDayOfRefund;
        CreditPeriod = creditPeriod;
        CreditPercentCommissionForUseAfterMinus = creditPercentCommissionForUseAfterMinus;
        CreditLimitMinus = creditLimitMinus;
    }

    public int WithdrawalLimit { get; internal set; }
    public int TransferLimit { get; internal set; }
    public int PercentageBalance { get; internal set; }
    public decimal MinDepositInterestRate { get; internal set; }
    public decimal MidDepositInterestRate { get; internal set; }
    public decimal MaxDepositInterestRate { get; internal set; }
    public decimal DepositTransferPercent { get; internal set; }
    public int DepositPeriod { get; internal set; }
    public decimal CreditRefundPercentAfterDayOfRefund { get; internal set; }
    public int CreditPeriod { get; internal set; }
    public decimal CreditPercentCommissionForUseAfterMinus { get; internal set; }
    public int CreditLimitMinus { get; internal set; }

    public void ChangeWithdrawalLimit(int newWithdrawalLimit)
    {
        if (newWithdrawalLimit < IncorrectNumber || newWithdrawalLimit > MaxWithdrawalLimit)
            throw new BankExceptions("New limit isn't valid");
        WithdrawalLimit = newWithdrawalLimit;
    }

    public void ChangeTransferLimit(int newTransferLimit)
    {
        if (newTransferLimit < IncorrectNumber || newTransferLimit > MaxWithdrawalLimit)
            throw new BankExceptions("New limit isn't valid");
        TransferLimit = newTransferLimit;
    }

    public void ChagePercentageBalance(int percentageBalance)
    {
    }

    public void ChangeMinDepositInterestRate(decimal newMin)
    {
        if (newMin < IncorrectNumber && (newMin > MidDepositInterestRate || newMin > MaxDepositInterestRate))
            throw new BankExceptions("New minDepositInterestRate isn't valid");
        MinDepositInterestRate = newMin;
    }

    public void ChangeMidDepositInterestRate(decimal newMid)
    {
        if (newMid < IncorrectNumber && (newMid > MaxDepositInterestRate || newMid < MinDepositInterestRate))
            throw new BankExceptions("New minDepositInterestRate isn't valid");
        MidDepositInterestRate = newMid;
    }

    public void ChangeMaxDepositInterestRate(decimal newMax)
    {
        if (newMax < IncorrectNumber && (newMax > MidDepositInterestRate || newMax > MinDepositInterestRate))
            throw new BankExceptions("New minDepositInterestRate isn't valid");
        MaxDepositInterestRate = newMax;
    }

    public void ChangeDepositTransferPercent(decimal newPercent)
    {
        if (newPercent < IncorrectNumber) throw new BankExceptions("New deposit Transfer Percent isn't valid");
        DepositTransferPercent = newPercent;
    }

    public void ChangeDepositPeriod(int newPeriod)
    {
        if (newPeriod < MinPeriod) throw new BankExceptions("New deposit Period isn't valid");
        DepositPeriod = newPeriod;
    }

    public void ChangeCreditRefundPercentAfterDayOfRefund(decimal newPercent)
    {
        if (newPercent > PercentInBank) throw new BankExceptions("New credit Refund isn't valid");
        CreditRefundPercentAfterDayOfRefund = newPercent;
    }

    public void ChangeCreditPeriod(int newPeriod)
    {
        if (newPeriod < MinPeriod) throw new BankExceptions("New credit Period isn't valid");
        CreditPeriod = newPeriod;
    }

    public void ChangeCreditPercentCommissionForUseAfterMinus(decimal newPercent)
    {
        if (newPercent > PercentInBank) throw new BankExceptions("New credit Percent isn't valid");
        CreditPercentCommissionForUseAfterMinus = newPercent;
    }

    public void ChangeCreditLimitMinus(int newLimit)
    {
        if (newLimit > MaxWithdrawalLimit) throw new BankExceptions("New credit limit after limit isn't valid");
        CreditLimitMinus = newLimit;
    }
}