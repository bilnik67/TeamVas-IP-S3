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
        public EducationalContext(DbContextOptions<EducationalContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
    }
}
