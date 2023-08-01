using Banks.Accounts;
using Banks.Banks;
using Banks.Exceptions;
using Banks.Transactions;
using Banks.Users;

namespace Banks;

internal static class Program
{
    private static void Main()
    {
        CentralBank centralBank = new CentralBank();
        BankParameters bankParameters = new BankParameters(
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
        User trusted = centralBank.CreateUser("Aliosha", "Popovich", 1234567,  "Kiev", bank);
        User untrusted = centralBank.CreateUser("Tugarin", "Zmei", 12344545,  "Kiev", bank);
        while (true)
        {
            switch (Console.ReadLine())
            {
                case "/NewDebitAccount":
                    try
                    {
                        Console.WriteLine("Choose trusted or untrusted user:(1 or 2)");
                        string answer = Console.ReadLine();
                        if (answer == "1")
                        {
                            var debit = centralBank.CreateDebit(trusted, bank, 100000, 1);
                            Console.WriteLine(debit.Money);
                        }
                        else if (answer == "2")
                        {
                            centralBank.CreateDebit(untrusted, bank, 10000, 2);
                        }
                        else
                        {
                            throw new BankExceptions("Unknown user");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case "/NewCreditAccount":
                    try
                    {
                        Console.WriteLine("Choose trusted or untrusted user:(1 or 2)");
                        string answer = Console.ReadLine();
                        if (answer == "1")
                        {
                            centralBank.CreateCredit(trusted, bank, 10000, 1);
                        }
                        else if (answer == "2")
                        {
                            centralBank.CreateCredit(trusted, bank, 10000, 2);
                        }
                        else
                        {
                            throw new BankExceptions("Unknown user");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case "/NewDepositAccount":
                    try
                    {
                        Console.WriteLine("Choose trusted or untrusted user:(1 or 2)");
                        string answer = Console.ReadLine();
                        if (answer == "1")
                        {
                            centralBank.CreateDeposit(trusted, bank, 10000, 1);
                        }
                        else if (answer == "2")
                        {
                            centralBank.CreateDeposit(trusted, bank, 10000, 2);
                        }
                        else
                        {
                            throw new BankExceptions("Unknown user");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case "/WithdrawalTransaction":
                    try
                    {
                        Console.WriteLine(
                            "Choose account:" +
                            "Debit - 1" +
                            "Credit - 2" +
                            "Deposit - 3");
                        int answer = Convert.ToInt32(Console.ReadLine());
                        if (answer == 1)
                        {
                            Console.WriteLine("Enter debit id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Account debit = centralBank.GetAccount(id);
                            int idForTransaction = Convert.ToInt32(Console.ReadLine());
                            centralBank.CreateWithdrawalTransaction(debit, 1000, idForTransaction);
                            Console.WriteLine(debit.Money);
                        }

                        if (answer == 2)
                        {
                            Console.WriteLine("Enter credit id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Account credit = centralBank.GetAccount(id);
                            int idForTransaction = Convert.ToInt32(Console.ReadLine());
                            centralBank.CreateWithdrawalTransaction(credit, 1000, idForTransaction);
                            Console.WriteLine(credit.Money);
                        }

                        if (answer == 3)
                        {
                            Console.WriteLine("Enter depoit id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Account deposit = centralBank.GetAccount(id);
                            int idForTransaction = Convert.ToInt32(Console.ReadLine());
                            centralBank.CreateWithdrawalTransaction(deposit, 1000, idForTransaction);
                            Console.WriteLine(deposit.Money);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error");
                    }

                    break;
                case "/RefillTransaction":
                    try
                    {
                        Console.WriteLine(
                            "Choose account:" +
                            "Debit - 1" +
                            "Credit - 2" +
                            "Deposit - 3");
                        int answer = Convert.ToInt32(Console.ReadLine());
                        if (answer == 1)
                        {
                            Console.WriteLine("Enter debit id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Account debit = centralBank.GetAccount(id);
                            int idForTransaction = Convert.ToInt32(Console.ReadLine());
                            centralBank.CreateRefillTransaction(debit, 1000, idForTransaction);
                            Console.WriteLine(debit.Money);
                        }

                        if (answer == 2)
                        {
                            Console.WriteLine("Enter credit id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Account credit = centralBank.GetAccount(id);
                            int idForTransaction = Convert.ToInt32(Console.ReadLine());
                            centralBank.CreateRefillTransaction(credit, 1000, idForTransaction);
                            Console.WriteLine(credit.Money);
                        }

                        if (answer == 3)
                        {
                            Console.WriteLine("Enter deposit id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Account deposit = centralBank.GetAccount(id);
                            int idForTransaction = Convert.ToInt32(Console.ReadLine());
                            centralBank.CreateRefillTransaction(deposit, 1000, idForTransaction);
                            Console.WriteLine(deposit.Money);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case "/TransferTransaction":
                    try
                    {
                        Console.WriteLine(
                            "Choose account:" +
                            "Debit - 1" +
                            "Credit - 2" +
                            "Deposit - 3");
                        int answer = Convert.ToInt32(Console.ReadLine());
                        if (answer == 1)
                        {
                            Console.WriteLine("Enter debit id for first");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter debit id for second");
                            int id2 = Convert.ToInt32(Console.ReadLine());
                            Account debit = centralBank.GetAccount(id);
                            Account debit2 = centralBank.GetAccount(id2);
                            int idForTransaction = Convert.ToInt32(Console.ReadLine());
                            centralBank.CreateTransferTransaction(debit, debit2, 1000, idForTransaction);
                            Console.WriteLine(debit.Money);
                            Console.WriteLine(debit2.Money);
                        }

                        if (answer == 2)
                        {
                            Console.WriteLine("Enter debit id for first");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter debit id for second");
                            int id2 = Convert.ToInt32(Console.ReadLine());
                            Account credit = centralBank.GetAccount(id);
                            Account credit2 = centralBank.GetAccount(id2);
                            int idForTransaction = Convert.ToInt32(Console.ReadLine());
                            centralBank.CreateTransferTransaction(credit, credit2, 1000, idForTransaction);
                            Console.WriteLine(credit.Money);
                        }

                        if (answer == 3)
                        {
                            Console.WriteLine("Enter debit id for first");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter debit id for second");
                            int id2 = Convert.ToInt32(Console.ReadLine());
                            Account deposit = centralBank.GetAccount(id);
                            Account deposit2 = centralBank.GetAccount(id2);
                            int idForTransaction = Convert.ToInt32(Console.ReadLine());
                            centralBank.CreateTransferTransaction(deposit, deposit2, 1000, id);
                            Console.WriteLine(deposit.Money);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error");
                    }

                    break;
                case "/CancelTransaction":
                    try
                    {
                        Console.WriteLine("Enter id of transaction");
                        int idForTransaction = Convert.ToInt32(Console.ReadLine());
                        Transaction transaction = centralBank.GetTransaction(idForTransaction);
                        centralBank.CreateCancelTransaction(transaction);
                        Console.WriteLine(transaction.Money);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error");
                    }

                    break;
                case "/ChangeWithdrawalLimit":
                    try
                    {
                        centralBank.ChangeWithdrawalLimit(130000, bank);
                        Console.WriteLine(bank.BankParameters.WithdrawalLimit);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case "/ChangeCreditPeriod":
                    try
                    {
                        centralBank.ChangeCreditPeriod(120, bank);
                        Console.WriteLine(bank.BankParameters.CreditPeriod);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case "/ChangeDepositPeriod":
                    try
                    {
                        centralBank.ChangeDepositPeriod(120, bank);
                        Console.WriteLine(bank.BankParameters.DepositPeriod);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case "/ChangeTransferLimit":
                    try
                    {
                        centralBank.ChangeTransferLimit(150000, bank);
                        Console.WriteLine(bank.BankParameters.TransferLimit);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case "/ChangeLimitMinus":
                    try
                    {
                        centralBank.ChangeCreditLimitMinus(140000, bank);
                        Console.WriteLine(bank.BankParameters.CreditLimitMinus);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case "/ChangeMidDepositInterestRate":
                    try
                    {
                        centralBank.ChangeMidDepositInterestRate(8, bank);
                        Console.WriteLine(bank.BankParameters.MidDepositInterestRate);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case "/ChangeCreditPercentCommission":
                    try
                    {
                        centralBank.ChangeCreditPercentCommissionForUseAfterMinus(14, bank);
                        Console.WriteLine(bank.BankParameters.CreditPercentCommissionForUseAfterMinus);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case "/ChangeCreditRefundPercent":
                    try
                    {
                        centralBank.ChangeCreditRefundPercentAfterDayOfRefund(9, bank);
                        Console.WriteLine(bank.BankParameters.CreditRefundPercentAfterDayOfRefund);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case "/ChangeDepositTransferLimit":
                    try
                    {
                        centralBank.ChangeDepositTransferPercent(8, bank);
                        Console.WriteLine(bank.BankParameters.DepositTransferPercent);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case "/Subscribe":
                    try
                    {
                        centralBank.SubscribeEvent(trusted, bank);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                case "/SkipDays":
                    try
                    {
                        centralBank.SkipDays(10);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    break;
            }
        }
    }
}