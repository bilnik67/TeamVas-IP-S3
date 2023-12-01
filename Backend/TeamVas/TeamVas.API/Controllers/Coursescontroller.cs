using Microsoft.AspNetCore.Mvc;
using TeamVas.API.DTOs;
using TeamVas.BLogic.Models;
using TeamVas.BLogic.Services;

namespace TeamVas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Coursescontroller : ControllerBase
    {
        private readonly ICourseService _courseService;

        public Coursescontroller(ICourseService courseService)
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
            var course = _courseService.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }
    }
}
