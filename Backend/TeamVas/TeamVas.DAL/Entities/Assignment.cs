using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamVas.DAL.Entities
{
    public class Assignment
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        public void SetAssignmentModel(int id, string title, string description)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
        }

        public Assignment(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}
