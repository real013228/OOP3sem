namespace Banks.Models;

public class Passport
{
    public Passport(string passport)
    {
        if (passport.Length != 10 || !int.TryParse(passport, out int _)) throw new NullReferenceException();
        PassportName = passport;
    }

    public string PassportName { get; }
}