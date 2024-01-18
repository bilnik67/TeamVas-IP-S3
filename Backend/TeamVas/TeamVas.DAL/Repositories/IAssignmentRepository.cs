using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamVas.DAL.Entities;

namespace TeamVas.DAL.Repositories
{
    public interface IAssignmentRepository
    {
        List<Assignment> GetAllAssignments();
        Assignment GetAssignmentById(int assignmentId);
        Assignment AddAssignment(Assignment assignment);
        void UpdateAssignment(Assignment assignment);
        void DeleteAssignment(int assignmentId);
        void AddSubmission(AssignmentSubmissionService submission);
        IEnumerable<AssignmentSubmissionService> GetSubmissionsByAssignmentId(int assignmentId);
    }
}
