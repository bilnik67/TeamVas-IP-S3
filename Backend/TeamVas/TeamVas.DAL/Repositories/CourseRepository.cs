using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamVas.DAL.Entities;

namespace TeamVas.DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly EducationalContext _context;

        public CourseRepository(EducationalContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Course.ToListAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            var course = await _context.Course
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                throw new KeyNotFoundException($"A course with ID {courseId} was not found.");
            }

            return course;
        }

        public async Task<Course> AddCourseAsync(Course course)
        {
            _context.Course.Add(course);
            await _context.SaveChangesAsync();
            return course; 
        }

        public async Task UpdateCourseAsync(Course course)
        {
            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            var course = await _context.Course.FindAsync(courseId);
            if (course != null)
            {
                _context.Course.Remove(course);
                await _context.SaveChangesAsync();
            }
        }
    }
}
