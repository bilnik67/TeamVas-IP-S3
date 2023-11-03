using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamVas.BLogic.Models;

namespace TeamVas.BLogic.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseModel>> GetAllCoursesAsync();
        Task<CourseModel> GetCourseByIdAsync(int courseId);
    }
}
