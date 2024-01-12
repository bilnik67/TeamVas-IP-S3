using System.Runtime.Serialization;

namespace Exceptions.Assignments
{
    [Serializable]
    public class AssignmentNotFoundException : Exception
    {
        public AssignmentNotFoundException(string? message) : base(message)
        {
        }

      
    }
}