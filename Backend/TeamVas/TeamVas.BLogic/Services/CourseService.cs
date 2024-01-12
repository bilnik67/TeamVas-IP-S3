using Exceptions.Courses;
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

            if (courses == null)
            {
                throw new InvalidOperationException("No courses available.");
            }

            return courses.Select(c => new CourseModel(c.Id, c.Name, c.Description)).ToList();
        }

        public CourseModel GetCourseById(int courseId)
        {
            var course = _courseRepository.GetCourseById(courseId);

            if (course == null)
            {
                return null;
            }

            return new CourseModel(course.Id, course.Name, course.Description);
        }

        public CourseModel AddCourse(CourseModel courseModel)
        {
            var course = new Course(courseModel.Id ,courseModel.Name, courseModel.Description);

            var addedCourse = _courseRepository.AddCourse(course);

            return new CourseModel(addedCourse.Id, addedCourse.Name, addedCourse.Description);
        }

        public void UpdateCourse(CourseModel courseModel)
        {
            var existingCourse = _courseRepository.GetCourseById(courseModel.Id);

            if (existingCourse != null)
            {
                existingCourse.SetCourseModel(courseModel.Id, courseModel.Name, courseModel.Description);

                _courseRepository.UpdateCourse(existingCourse);

            }
            else
            {
                throw new CourseNotFoundException($"Course with ID {courseModel.Id} not found.");
            }
        }

        public void DeleteCourse(int courseId)
        {
            _courseRepository.DeleteCourse(courseId);
        }

    }
}
