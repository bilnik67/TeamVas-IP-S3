using System.Runtime.Serialization;

namespace Exceptions.Courses
{
    [Serializable]
    public class CourseNotFoundException : Exception
    {

        public CourseNotFoundException(string? message) : base(message)
        {
        }
        
    }
}