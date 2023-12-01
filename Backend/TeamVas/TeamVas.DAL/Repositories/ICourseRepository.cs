using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamVas.DAL.Entities;

namespace TeamVas.DAL.Repositories
{
    public interface ICourseRepository
    {
        List<Course> GetAllCourses();
        Course GetCourseById(int courseId);
        Course AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int courseId);
    }
}
