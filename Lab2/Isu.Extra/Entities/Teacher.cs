namespace Isu.Extra.Entities;

public class Teacher
{
    public Teacher(string teacherName)
    {
        TeacherName = teacherName;
        Id = Guid.NewGuid();
    }

    public string TeacherName { get; }

    public Guid Id { get; }
}