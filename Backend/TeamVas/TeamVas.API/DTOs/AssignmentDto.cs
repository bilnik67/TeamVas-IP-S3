namespace TeamVas.API.DTOs
{
    public class AssignmentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 

        public AssignmentDto(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
            
        }
        public AssignmentDto() 
        {
            Title = "";
            Description = "";
        }
    }
}
