using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamVas.BLogic.Models
{
    public class AssignmentModel
    {
        public int Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;


        public AssignmentModel(int id, string title, string description)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
        }

    }
}
