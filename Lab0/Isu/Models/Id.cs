using Isu.CustomExceptions;

namespace Isu.Models;

public class Id
{
    public int IdGenerator(int value)
    {
        return value + 100000;
    }
}