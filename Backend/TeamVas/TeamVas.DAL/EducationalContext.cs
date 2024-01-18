using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamVas.DAL.Entities;

namespace TeamVas.DAL
{
    public class EducationalContext : DbContext
    {

        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public EducationalContext(DbContextOptions<EducationalContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Course { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<AssignmentSubmissionService> Assignment_Submission { get; set; }
    }
}
