namespace TeamVas.API.DTOs
{
    public class CourseDto
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        public CourseDto(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            
        }
    }
}
