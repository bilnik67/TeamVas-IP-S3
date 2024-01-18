using Exceptions.Assignments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamVas.BLogic.Models;
using TeamVas.DAL.Entities;
using TeamVas.DAL.Repositories;

namespace TeamVas.BLogic.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public IEnumerable<AssignmentModel> GetAllAssignments()
        {
            var assignments =  _assignmentRepository.GetAllAssignments();

            if (assignments == null)
            {
                throw new InvalidOperationException("No assignments available.");
            }

            return assignments.Select(c => new AssignmentModel(c.Id, c.Title, c.Description)).ToList();
        }

        public AssignmentModel GetAssignmentById(int assignmentId)
        {
            var assignment = _assignmentRepository.GetAssignmentById(assignmentId);

            if (assignment == null)
            {
                throw new AssignmentNotFoundException($"Assignment with ID {assignmentId} not found.");
            }

            return new AssignmentModel(assignment.Id, assignment.Title, assignment.Description);
        }

        public AssignmentModel AddAssignment(AssignmentModel assignmentModel)
        {
            var assignment = new Assignment(assignmentModel.Id ,assignmentModel.Title, assignmentModel.Description);

            var addedAssignment = _assignmentRepository.AddAssignment(assignment);

            return new AssignmentModel(addedAssignment.Id, addedAssignment.Title, addedAssignment.Description);
        }

        public void UpdateAssignment(AssignmentModel assignmentModel)
        {
            var existingAssignment = _assignmentRepository.GetAssignmentById(assignmentModel.Id);

            if (existingAssignment != null)
            {
                existingAssignment.SetAssignmentModel(assignmentModel.Id, assignmentModel.Title, assignmentModel.Description);

                _assignmentRepository.UpdateAssignment(existingAssignment);

            }
            else
            {
                throw new AssignmentNotFoundException($"Assignment with ID {assignmentModel.Id} not found.");
            }
        }

        public void DeleteAssignment(int assignmentId)
        {
            _assignmentRepository.DeleteAssignment(assignmentId);
        }
        public void AddSubmission(int assignmentId, string content)
        {
            var submission = new AssignmentSubmissionService
            {
                assignmentid = assignmentId,
                Content = content
            };

            _assignmentRepository.AddSubmission(submission);
        }

        public IEnumerable<AssignmentSubmissionService> GetSubmissionsByAssignmentId(int assignmentId)
        {
            return _assignmentRepository.GetSubmissionsByAssignmentId(assignmentId);
        }

    }
}
