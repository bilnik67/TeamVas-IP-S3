using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamVas.DAL.Entities;
using Exceptions.Assignments;

namespace TeamVas.DAL.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly EducationalContext _context;

        public AssignmentRepository(EducationalContext context)
        {
            _context = context;
        }

        public List<Assignment> GetAllAssignments()
        {
            var assignments = _context.Assignment.ToList();

            if (!assignments.Any()) 
            {
                return new List<Assignment>();
            }

            return assignments;
        }

        public Assignment GetAssignmentById(int assignmentId)
        {
            var assignment = _context.Assignment.FirstOrDefault(c => c.Id == assignmentId);

            if (assignment == null)
            {
                throw new AssignmentNotFoundException($"Assignment with ID {assignmentId} not found.");
            }

            return assignment;
        }

        public Assignment AddAssignment(Assignment assignment)
        {
            _context.Assignment.Add(assignment);
            _context.SaveChanges();
            return assignment; 
        }

        public void UpdateAssignment(Assignment assignment)
        {
            var existingAssignment = _context.Assignment.Find(assignment.Id);
            if (existingAssignment != null)
            {
                _context.Entry(existingAssignment).CurrentValues.SetValues(assignment);
            }
            else
            {
                throw new AssignmentNotFoundException($"Assignment with ID {assignment.Id} not found.");
            }

            _context.SaveChanges();
        }

        public void DeleteAssignment(int assignmentId)
        {
            var assignment = _context.Assignment.Find(assignmentId);
            if (assignment != null)
            {
                _context.Assignment.Remove(assignment);
                _context.SaveChanges();
            }
        }
        public void AddSubmission(AssignmentSubmissionService submission)
        {
            _context.Assignment_Submission.Add(submission);
            _context.SaveChanges();
        }

        public IEnumerable<AssignmentSubmissionService> GetSubmissionsByAssignmentId(int assignmentId)
        {
            return _context.Assignment_Submission
                           .Where(submission => submission.assignmentid == assignmentId)
                           .ToList();
        }
    }
}
