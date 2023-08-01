using Banks.Accounts;
using Banks.Banks;
using Banks.Exceptions;
using Banks.Users;
using Xunit;

namespace Banks.Test;

public class BackupTest
{
    private readonly CentralBank centralBank;

    public BackupTest()
    {
        centralBank = new CentralBank();
    }

    [Fact]
    public void CreateBank_User_BankParameters_Debit_Deposit_Credit()
    {
        var bankParameters = new BankParameters(
            140000,
            140000,
            10,
            5,
            7,
            10,
            7,
            100,
            10,
            100,
            15,
            -100000);
        Bank bank = centralBank.CreateBank("Tinkoff", bankParameters);
        User user = centralBank.CreateUser("Aliosha", "Popovich", 1234567,  "Kiev", bank);
        var debit = centralBank.CreateDebit(user, bank, 100000, 1);
        var credit = centralBank.CreateCredit(user, bank, 10000, 1);
        var deposit = centralBank.CreateDeposit(user, bank, 10000, 1);
        Assert.Equal(100000, debit.Money);
        Assert.Equal(10000, credit.Money);
        Assert.Equal(1, deposit.Id);
    }

    [Fact]
    public void CatchExceptionOnWithdrawal()
    {
        var bankParameters = new BankParameters(
            140000,
            140000,
            10,
            5,
            7,
            10,
            7,
            100,
            10,
            100,
            15,
            -100000);
        Bank bank = centralBank.CreateBank("Tinkoff", bankParameters);
        User user = centralBank.CreateUser("Aliosha", "Popovich", 1234567,  "Kiev", bank);
        var deposit = centralBank.CreateDeposit(user, bank, 100000, 1);
        int id = 1;
        Account debitId = centralBank.GetAccount(id);
        int idForTransaction = 10;
        Assert.Throws<BankExceptions>(() => centralBank.CreateWithdrawalTransaction(debitId, 1000000, idForTransaction));
    }
}