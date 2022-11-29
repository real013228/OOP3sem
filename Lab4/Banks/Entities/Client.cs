using System.Security.Cryptography.X509Certificates;
using Banks.Abstractions;

namespace Banks.Entities;

public class Client
{
    private readonly List<IBankAccount> _accounts;
    private string _passport;
    private string _address;

    private Client(string firstName, string lastName, string passport, string address, List<IBankAccount> accounts, bool isSus)
    {
        FirstName = firstName;
        LastName = lastName;
        _passport = passport;
        _address = address;
        _accounts = accounts;
        IsSus = isSus;
    }

    public static ClientBuilder Builder => new ClientBuilder();
    public bool IsSus { get; }
    public IReadOnlyList<IBankAccount> Accounts => _accounts;
    public string FirstName { get; }
    public string LastName { get; }

    public class ClientBuilder
    {
        private string _passport = string.Empty;
        private string _address = string.Empty;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private List<IBankAccount> _accounts = new List<IBankAccount>();
        private bool _isSus = true;

        public ClientBuilder WithAccounts(List<IBankAccount> accounts)
        {
            _accounts = accounts;
            return this;
        }

        public ClientBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public ClientBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public ClientBuilder WithPassport(string passport)
        {
            if (passport.Length != 8 || !int.TryParse(passport, out int _)) throw new NullReferenceException();
            _passport = passport;
            return this;
        }

        public ClientBuilder WithAddress(string address)
        {
            _address = address;
            return this;
        }

        public ClientBuilder IsSus(bool isSus)
        {
            _isSus = isSus;
            return this;
        }

        public Client Build()
        {
            if (_firstName == string.Empty || _lastName == string.Empty)
                throw new NullReferenceException();
            return new Client(_firstName, _lastName, _passport, _address, _accounts, _isSus);
        }
    }
}