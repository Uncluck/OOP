using Banks.Banks;
using Banks.Exceptions;

namespace Banks.Users;

public class UserBuilder
{
    private const int MaxNumber = 20;
    private const int IncorrectNumber = 0;
    private string _name;
    private string _surname;
    private int _passport;
    private string _address;
    private User _user;

    public UserBuilder SetName(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new BankExceptions("Name is null");
        _name = name;
        return this;
    }

    public UserBuilder SetSurname(string surname)
    {
        if (string.IsNullOrEmpty(surname)) throw new BankExceptions("Name is null");
        _surname = surname;
        return this;
    }

    public UserBuilder SetPassport(int passport)
    {
        if (passport < IncorrectNumber) throw new BankExceptions("Passport is null");
        _passport = passport;
        return this;
    }

    public UserBuilder SetAddress(string address)
    {
        if (string.IsNullOrEmpty(address)) throw new BankExceptions("Passport is null");
        _address = address;
        return this;
    }

    public User Create(Bank bank)
    {
        _user = new User(_name, _surname, _passport, _address, bank);
        return _user;
    }
}