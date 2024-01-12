namespace TeamVas.API.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 

        public CourseDto(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            
        }
        public CourseDto() 
        {
            Name = "";
            Description = "";
        }
    }
}
