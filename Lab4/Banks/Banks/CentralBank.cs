using Banks.Accounts;
using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Transactions;
using Banks.Users;

namespace Banks.Banks;

public class CentralBank : ICentralBank
{
    private const int MinPeriod = 100;
    private const int MaxWithdrawalLimit = -150000;
    private const decimal PercentInBank = 10;
    private const decimal IncorrectNumber = 0;
    private List<Bank> _banks;
    private List<User> _users;
    private List<Transaction> _transactions;
    private List<Account> _accounts;
    private TimeService _timeService;
    public CentralBank()
    {
        _banks = new List<Bank>();
        _users = new List<User>();
        _transactions = new List<Transaction>();
        _accounts = new List<Account>();
        _timeService = new TimeService();
    }

    public DateTime DateCurrentTime { get; set; }

    public Bank CreateBank(string name, BankParameters bankParameters)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new BankExceptions("Incorrect name of bank");
        if (bankParameters is null) throw new BankExceptions("Bank Parameters is null");
        var bank = new Bank(name, bankParameters);
        _banks.Add(bank);
        return bank;
    }

    public User CreateUser(string name, string surname, int passport, string address, Bank bank)
    {
        var user = new UserBuilder().SetName(name).SetSurname(surname).SetPassport(passport)
            .SetAddress(address).Create(bank);
        _users.Add(user);
        bank.AddUserForBank(user);
        return user;
    }

    public Debit CreateDebit(User user, Bank bank, decimal money, int id)
    {
        var debitForUser = new Debit(user, bank, money, id);
        user.AddAccountForUser(debitForUser);
        _accounts.Add(debitForUser);
        return debitForUser;
    }

    public Deposit CreateDeposit(User user, Bank bank, decimal money, int id)
    {
        var depositForUser = new Deposit(user, bank, money, id);
        user.AddAccountForUser(depositForUser);
        _accounts.Add(depositForUser);
        return depositForUser;
    }

    public Credit CreateCredit(User user, Bank bank, decimal money, int id)
    {
        var creditForUser = new Credit(user, bank, money, id);
        user.AddAccountForUser(creditForUser);
        _accounts.Add(creditForUser);
        return creditForUser;
    }

    public Bank FindBank(string name)
    {
         return _banks.SingleOrDefault(bank => bank.Name == name);
    }

    public Bank GetBank(string name)
    {
        return FindBank(name) ?? throw new BankExceptions("bank isn't valid");
    }

    public User FindUser(int passport)
    {
        return _users.SingleOrDefault(user => user.Passport == passport);
    }

    public User GetUser(int passport)
    {
        return FindUser(passport) ?? throw new BankExceptions("User isn't valid");
    }

    public Account FindAccount(int id)
    {
        return _accounts.SingleOrDefault(debit => debit.Id == id);
    }

    public Account GetAccount(int id)
    {
        return FindAccount(id) ?? throw new ArgumentNullException(nameof(id));
    }

    public Transaction FindTransaction(int id)
    {
        return _transactions.SingleOrDefault(transaction => transaction.Id == id);
    }

    public Transaction GetTransaction(int id)
    {
        return FindTransaction(id) ?? throw new BankExceptions("Transaction isn't valid");
    }

    public Transaction CreateWithdrawalTransaction(Account userAccount, decimal money, int id)
    {
        var transaction = new WithdrawalTransaction(userAccount, userAccount, money, id);
        transaction.ExecuteTransaction();
        _transactions.Add(transaction);
        return transaction;
    }

    public Transaction CreateTransferTransaction(Account userAccount, Account recipient, decimal money, int id)
    {
        var transaction = new TransferTransaction(userAccount, recipient, money, id);
        transaction.ExecuteTransaction();
        _transactions.Add(transaction);
        return transaction;
    }

    public Transaction CreateRefillTransaction(Account userAccount, decimal money, int id)
    {
        var transaction = new RefillTransaction(userAccount, userAccount, money, id);
        transaction.ExecuteTransaction();
        _transactions.Add(transaction);
        return transaction;
    }

    public void CreateCancelTransaction(Transaction transaction)
    {
        transaction?.CancelTransaction();
    }

    public void ChangeWithdrawalLimit(int newWithdrawalLimit, Bank bank)
    {
        bank.ChangeWithdrawalLimit(newWithdrawalLimit);
    }

    public void ChangeTransferLimit(int newTransferLimit, Bank bank)
    {
        bank.ChangeTransferLimit(newTransferLimit);
    }

    public void ChangeMinDepositInterestRate(decimal newMin, Bank bank)
    {
       bank.ChangeMinDepositInterestRate(newMin);
    }

    public void ChangeMidDepositInterestRate(decimal newMid, Bank bank)
    {
        bank.ChangeMidDepositInterestRate(newMid);
    }

    public void ChangeMaxDepositInterestRate(decimal newMax, Bank bank)
    {
        bank.ChangeMaxDepositInterestRate(newMax);
    }

    public void ChangeDepositTransferPercent(decimal newPercent, Bank bank)
    {
        bank.ChangeDepositTransferPercent(newPercent);
    }

    public void ChangeDepositPeriod(int newPeriod, Bank bank)
    {
        bank.ChangeDepositPeriod(newPeriod);
    }

    public void ChangeCreditRefundPercentAfterDayOfRefund(decimal newPercent, Bank bank)
    {
        bank.ChangeCreditRefundPercentAfterDayOfRefund(newPercent);
    }

    public void ChangeCreditPeriod(int newPeriod, Bank bank)
    {
        bank.ChangeCreditPeriod(newPeriod);
    }

    public void ChangeCreditPercentCommissionForUseAfterMinus(decimal newPercent, Bank bank)
    {
        bank.ChangeCreditPercentCommissionForUseAfterMinus(newPercent);
    }

    public void ChangeCreditLimitMinus(int newLimit, Bank bank)
    {
        bank.ChangeCreditLimitMinus(newLimit);
    }

    public void SubscribeEvent(User user, Bank bank)
    {
        bank.SubscribeEvent(user);
    }

    public void UnsubscribeEvent(User user, Bank bank)
    {
        bank.UnsubscribeEvent(user);
    }

    public void SkipDays(int days)
    {
        DateTime currDate = _timeService.SkipDay(days);
        foreach (Account account in _accounts)
        {
            account.UpdateDays(days, currDate);
        }
    }
}