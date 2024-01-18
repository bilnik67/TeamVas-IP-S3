using Exceptions.Assignments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TeamVas.API.DTOs;
using TeamVas.BLogic.Models;
using TeamVas.BLogic.Services;

namespace TeamVas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<AssignmentDto>> GetAllAssignments()
        {
            var assignments = _assignmentService.GetAllAssignments();
            return Ok(assignments);
        }

        [HttpGet("{id}")]
        public ActionResult<AssignmentDto> GetAssignment(int id)
        {
            try
            {
                var assignment = _assignmentService.GetAssignmentById(id);
                return Ok(assignment);
            }
            catch (AssignmentNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", ex });
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult<AssignmentDto> AddAssignment([FromBody] AssignmentDto assignmentDto)
        {
            try
            {
                var assignmentmodel = ConvertToAssignmentModel(assignmentDto);

                var assignment = _assignmentService.AddAssignment(assignmentmodel);
                return CreatedAtAction(nameof(GetAssignment), new { id = assignment.Id }, assignment);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while adding the assignment." });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult UpdateAssignment([FromBody] AssignmentDto assignmentDto)
        {
            try
            {
                var assignmentModel = ConvertToAssignmentModel(assignmentDto);

                _assignmentService.UpdateAssignment(assignmentModel);
                return NoContent();
            }
            catch (AssignmentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while updating the assignment." });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult DeleteAssignment(int id)
        {
            try
            {
                var assignment = _assignmentService.GetAssignmentById(id);
                if (assignment == null)
                {
                    return NotFound();
                }

                _assignmentService.DeleteAssignment(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the assignment." });
            }
        }
        [HttpGet("{assignmentId}/submissions")]
        public ActionResult<IEnumerable<AssignmentSubmissionDto>> GetSubmissionsByAssignmentId(int assignmentId)
        {
            try
            {
                var submissions = _assignmentService.GetSubmissionsByAssignmentId(assignmentId);
                return Ok(submissions);
            }
            catch (AssignmentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error getting submissions for the assignment.", ex });
            }
        }
        [HttpPost("{assignmentId}/submissions")]
        public ActionResult SubmitAssignment(int assignmentId, [FromBody] AssignmentSubmissionDto submissionDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(submissionDto.Content))
                {
                    return BadRequest("Submission content cannot be empty.");
                }

                _assignmentService.AddSubmission(assignmentId, submissionDto.Content);

                return Ok("Submission successfully added.");
            }
            catch (AssignmentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error submitting assignment.", ex });
            }
        }
        private static AssignmentModel ConvertToAssignmentModel(AssignmentDto assignmentDto)
        {
            return new AssignmentModel(assignmentDto.Id, assignmentDto.Title, assignmentDto.Description);
        }

    }
}
