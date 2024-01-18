using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamVas.DAL.Entities;

namespace TeamVas.BLogic.Models
{
    public class AssignmentSubmission
    {
        public int Id { get; set; }
        public int Assignment_Id { get; set; } 
        public string Content { get; set; } 
        public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;
        public Assignment Assignment { get; set; }
    }
}
