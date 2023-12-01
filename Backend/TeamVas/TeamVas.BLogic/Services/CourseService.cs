using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamVas.BLogic.Models;
using TeamVas.DAL.Repositories;

namespace TeamVas.BLogic.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IEnumerable<CourseModel> GetAllCourses()
        {
            var courses =  _courseRepository.GetAllCourses();
            return courses.Select(c => new CourseModel(c.Id, c.Name, c.Description)).ToList();
        }

        public CourseModel GetCourseById(int courseId)
        {
            var course = _courseRepository.GetCourseById(courseId);

            return new CourseModel(course.Id, course.Name, course.Description);
        }

    }
}
