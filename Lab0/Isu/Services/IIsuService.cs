using Isu.Entities;
using Isu.Models;

namespace Isu.Services
{
    public interface IIsuService
    {
        Group AddGroup(GroupName name);
        Student AddStudent(Group group, string name);

        Student GetStudent(int id);
        Student? FindStudent(int id);
        List<Student> FindStudents(GroupName groupName);
        List<Student> FindStudents(CourseNumber courseNumber);

        Group? FindGroup(GroupName groupName);
        List<Group> FindGroups(CourseNumber courseNumber);

        void ChangeStudentGroup(Student student, Group newGroup);
    }

    public class IsuService : IIsuService
    {
        private const int TabelNum = 100000;
        private const int MaxStudentsPerGroup = 20;
        private List<Group> _groups;
        private List<Student> _students;

        public IsuService()
        {
            _groups = new List<Group>();
            _students = new List<Student>();
        }

        public Group AddGroup(GroupName name)
        {
            var newGroup = new Group(name);
            _groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count >= MaxStudentsPerGroup)
            {
                throw new ReachMaxStudentPerGroupException();
            }

            var newStudent = new Student(TabelNum + StudentsCount(), name, group.NameOfGroup.Course, group.NameOfGroup);
            group.AddStudent(newStudent);
            _students.Add(newStudent);
            return newStudent;
        }

        public Student GetStudent(int id)
        {
            try
            {
                return _students[id - TabelNum];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Student? FindStudent(int id)
        {
            if (id - TabelNum < StudentsCount())
            {
                Student? student = _students[id - TabelNum];
                return student;
            }

            return null;
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            var students = new List<Student>();
            foreach (Group group in _groups)
            {
                if (group.NameOfGroup == groupName)
                {
                    foreach (Student student in group.Students)
                    {
                        students.Add(student);
                    }
                }
            }

            return students;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var students = new List<Student>();
            foreach (Student student in _students)
            {
                if (student.Course == courseNumber)
                {
                    students.Add(student);
                }
            }

            return students;
        }

        public Group? FindGroup(GroupName groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.NameOfGroup == groupName)
                {
                    return group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            var groups = new List<Group>();
            foreach (Group group in _groups)
            {
                if (group.NameOfGroup.Course == courseNumber)
                {
                    groups.Add(group);
                }
            }

            return groups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (var group in _groups)
            {
                if (group.NameOfGroup == student.NameOfGroup)
                {
                    group.RemoveStudent(student);
                }
            }

            newGroup.AddStudent(student);
            student.NameOfGroup = newGroup.NameOfGroup;
        }

        private int StudentsCount()
        {
            return _students.Count;
        }
    }
}
