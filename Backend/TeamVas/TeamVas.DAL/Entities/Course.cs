using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamVas.DAL.Entities
{
    public class Course
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        public Course(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
