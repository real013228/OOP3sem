using System.Reflection.Metadata.Ecma335;
using System.Xml;
using Isu.Extra.Builders;
using Isu.Extra.DomainExceptions;
using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public class Lesson
{
    public const double OneLessonDuration = 90.0;
    private const double Tolerance = 0.01;
    private const int MinutesInOneHour = 60;

    public Lesson(DateTime begin, DateTime end, string name, Teacher teacher, string classRoom)
    {
        if (Math.Abs((end - begin).TotalMinutes - OneLessonDuration) > Tolerance)
        {
            throw LessonException.InvalidDuration();
        }

        if (((begin.Hour * Lesson.MinutesInOneHour) + begin.Minute) % 90 != 0)
        {
            throw LessonException.InvalidBeginOfLesson();
        }

        Begin = begin;
        End = end;
        Name = name;
        Teacher = teacher;
        ClassRoom = classRoom;
    }

    public static IBeginTimeBuilder Builder => new LessonBuilder();
    public DateTime Begin { get; }
    public DateTime End { get; }
    public string Name { get; }
    public Teacher Teacher { get; }
    public string ClassRoom { get; }

    private class LessonBuilder : IBeginTimeBuilder, IEndTimeBuilder, INameBuilder, ITeacherBuilder, IClassRoomBuilder,
        ILessonBuilder
    {
        public LessonBuilder()
        {
            Begin = default(DateTime);
            End = default(DateTime);
            Name = null;
            Teacher = null;
            ClassRoom = null;
        }

        public DateTime? Begin { get; private set; }
        public DateTime? End { get; private set; }
        public string? Name { get; private set; }
        public Teacher? Teacher { get; private set; }
        public string? ClassRoom { get; private set; }

        public IEndTimeBuilder WithBeginTime(DateTime beginTime)
        {
            Begin = beginTime;
            return this;
        }

        public INameBuilder WithEndTime(DateTime endTime)
        {
            End = endTime;
            return this;
        }

        public ITeacherBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public IClassRoomBuilder WithTeacher(Teacher teacher)
        {
            Teacher = teacher;
            return this;
        }

        public ILessonBuilder WithClassRoom(string classRoom)
        {
            ClassRoom = classRoom;
            return this;
        }

        public Lesson Build()
        {
            return new Lesson(
                Begin ?? throw LessonException.InvalidOperationException(),
                End ?? throw LessonException.InvalidOperationException(),
                Name ?? throw LessonException.InvalidOperationException(),
                Teacher ?? throw LessonException.InvalidOperationException(),
                ClassRoom ?? throw LessonException.InvalidOperationException());
        }
    }
}