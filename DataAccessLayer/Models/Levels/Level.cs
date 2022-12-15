namespace DataAccessLayer.Models.Levels;

public class Level
{
    public Level(int levelValue)
    {
        LevelValue = levelValue;
    }

    protected Level() { }
    public Guid Id { get; set; }
    public int LevelValue { get; set; }
}