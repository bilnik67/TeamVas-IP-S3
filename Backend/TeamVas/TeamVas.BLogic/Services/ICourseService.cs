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
        IEnumerable<CourseModel> GetAllCourses();
        CourseModel GetCourseById(int courseId);
    }
}
