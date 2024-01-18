using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamVas.DAL.Entities
{
    public class AssignmentSubmissionService
    {
        public int Id { get; set; }
        public int assignmentid { get; set; } 
        public string Content { get; set; } 
        public DateTime submitted_on { get; set; } = DateTime.UtcNow;
        public Assignment Assignment { get; set; }
    }
}
