namespace TeamVas.API.DTOs
{
    public class AssignmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 

        public AssignmentDto(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            
        }
        public AssignmentDto() 
        {
            Name = "";
            Description = "";
        }
    }
}
