namespace Isu.Services
{
    [Serializable]
    public class ReachMaxStudentPerGroupException : Exception
    {
        public ReachMaxStudentPerGroupException() { }

        public ReachMaxStudentPerGroupException(string message)
            : base(message) { }

        public ReachMaxStudentPerGroupException(string message, Exception inner)
            : base(message, inner) { }
    }
}
