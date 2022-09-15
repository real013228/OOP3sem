using Isu.CustomExceptions;

namespace Isu.Models;

public class Id
{
    public int IdGenerator(int value)
    {
        if (value < 0 || value > 900000)
        {
            throw new InvalidIdValueException(value.ToString());
        }

        return value + 100000;
    }
}