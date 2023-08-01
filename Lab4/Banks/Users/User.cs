using Banks.Accounts;
using Banks.Banks;
using Banks.Exceptions;

namespace Banks.Users;
public class User
    {
        private const int MaxNumber = 15;
        private const int IncorrectNumber = 0;
        private List<Account> _accounts;

        public User(string name, string surname, int passport, string address, Bank bank)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new BankExceptions("Name isn't valid");
            if (passport < IncorrectNumber) throw new BankExceptions("Passport doesn't exist");
            if (string.IsNullOrWhiteSpace(surname)) throw new BankExceptions("surname isn't valid");

            _accounts = new List<Account>();
            Name = name;
            Surname = surname;
            Passport = passport;
            Address = address;
            Bank = bank;
        }

        public string Name { get; }
        public string Surname { get; }
        public int Passport { get; }
        public int NumberPhone { get; private set; }
        public string Address { get; private set; }
        public Bank Bank { get; set; }

        public bool TrustedUnstrustad => Address == null;
        public void SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) throw new BankExceptions("Address isn't valid");
            Address = address;
        }

        public void AddAccountForUser(Account account)
        {
            if (account is null)
                throw new BankExceptions("Account is null");

            _accounts.Add(account);
        }

        public void Update(string msg)
        {
            Console.WriteLine($"{Name} {Surname} : {msg}");
        }

        public void SubscribeEvent()
        {
            Bank.EventBankParameterChanged += ReceiveNotification;
        }

        public void UnsubscribeEvent()
        {
            Bank.EventBankParameterChanged -= ReceiveNotification;
        }

        private void ReceiveNotification(object notifier, NotificationEventArgs e)
        {
            Console.WriteLine($"{Name} receive - {e.Message}");
        }
    }