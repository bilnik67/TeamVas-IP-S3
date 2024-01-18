using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamVas.BLogic.Models;
using TeamVas.DAL.Entities;

namespace TeamVas.BLogic.Services
{
    public interface IAssignmentService
    {
        IEnumerable<AssignmentModel> GetAllAssignments();
        AssignmentModel GetAssignmentById(int assignmentId);
        AssignmentModel AddAssignment(AssignmentModel assignmentModel);
        void UpdateAssignment(AssignmentModel assignmentModel);
        void DeleteAssignment(int assignmentId);
        void AddSubmission(int assignmentId, string content);
        IEnumerable<AssignmentSubmissionService> GetSubmissionsByAssignmentId(int assignmentId);
    }
}
