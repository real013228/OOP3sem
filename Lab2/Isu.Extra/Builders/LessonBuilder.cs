using Isu.Extra.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Builders;

public interface IBeginTimeBuilder
{
    IEndTimeBuilder WithBeginTime(DateTime beginTime);
}

public interface IEndTimeBuilder
{
    INameBuilder WithEndTime(DateTime endTime);
}

public interface INameBuilder
{
    ITeacherBuilder WithName(string name);
}

public interface ITeacherBuilder
{
    IClassRoomBuilder WithTeacher(Teacher teacher);
}

public interface IClassRoomBuilder
{
    ILessonBuilder WithClassRoom(string classRoom);
}

public interface ILessonBuilder
{
    Lesson Build();
}
