using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamVas.DAL.Entities;
using Exceptions.Courses;

namespace TeamVas.DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly EducationalContext _context;

        public CourseRepository(EducationalContext context)
        {
            _context = context;
        }

        public List<Course> GetAllCourses()
        {
            var courses = _context.Course.ToList();

            if (!courses.Any()) 
            {
                throw new CourseNotFoundException("No courses available.");
            }

            return courses;
        }

        public Course GetCourseById(int courseId)
        {
            var course = _context.Course.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                throw new CourseNotFoundException($"Course with ID {courseId} not found.");
            }

            return course;
        }

        public Course AddCourse(Course course)
        {
            _context.Course.Add(course);
            _context.SaveChanges();
            return course; 
        }

        public void UpdateCourse(Course course)
        {
            var existingCourse = _context.Course.Find(course.Id);
            if (existingCourse != null)
            {
                _context.Entry(existingCourse).CurrentValues.SetValues(course);
            }
            else
            {
                throw new CourseNotFoundException($"Course with ID {course.Id} not found.");
            }

            _context.SaveChanges();
        }

        public void DeleteCourse(int courseId)
        {
            var course = _context.Course.Find(courseId);
            if (course != null)
            {
                _context.Course.Remove(course);
                _context.SaveChanges();
            }
        }
    }
}
