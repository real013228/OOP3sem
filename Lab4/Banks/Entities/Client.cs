using System.Security.Cryptography.X509Certificates;
using Banks.Abstractions;
using Banks.Models;

namespace Banks.Entities;

public class Client
{
    private Client(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        IsSus = false;
        Accounts = new AccountCollection(new List<IBankAccount>());
        Id = Guid.NewGuid();
    }

    public static ClientBuilder Builder => new ClientBuilder();
    public AccountCollection Accounts { get; }
    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public Passport? Passport { get; private set; }
    public string? Address { get; private set; }
    public bool IsSus { get; private set; }

    public void VerifyClient(Passport passport, string address)
    {
        Passport = passport;
        Address = address;
        IsSus = false;
    }

    public class ClientBuilder
    {
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;

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

        public Client Build()
        {
            if (_firstName == string.Empty || _lastName == string.Empty)
                throw new NullReferenceException();
            return new Client(_firstName, _lastName);
        }
    }
}