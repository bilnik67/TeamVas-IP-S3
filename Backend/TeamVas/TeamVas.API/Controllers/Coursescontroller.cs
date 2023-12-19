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
    }
}
