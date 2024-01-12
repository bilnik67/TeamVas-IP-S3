using Exceptions.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TeamVas.API.DTOs;
using TeamVas.BLogic.Models;
using TeamVas.BLogic.Services;

namespace TeamVas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetAllCourses()
        {
            var courses = _courseService.GetAllCourses();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public ActionResult<CourseDto> GetCourse(int id)
        {
            try
            {
                var course = _courseService.GetCourseById(id);
                if (course == null)
                {
                    return NotFound();
                }
                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request." });
            }
        }
        [HttpPost]
        public ActionResult<CourseDto> AddCourse([FromBody] CourseDto courseDto)
        {
            try
            {
                var coursemodel = ConvertToCourseModel(courseDto);

                var course = _courseService.AddCourse(coursemodel);
                return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while adding the course." });
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCourse([FromBody] CourseDto courseDto)
        {
            try
            {
                var courseModel = ConvertToCourseModel(courseDto);

                _courseService.UpdateCourse(courseModel);
                return NoContent();
            }
            catch (CourseNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while updating the course." });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCourse(int id)
        {
            try
            {
                var course = _courseService.GetCourseById(id);
                if (course == null)
                {
                    return NotFound();
                }

                _courseService.DeleteCourse(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the course." });
            }
        }
        private CourseModel ConvertToCourseModel(CourseDto courseDto)
        {
            return new CourseModel(courseDto.Id, courseDto.Name, courseDto.Description);
        }

    }
}
