using System.Globalization;

namespace JoraClassLibrary
{
    public class ProjectTask
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public PriorityTaskEnum Priority { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? Deadline { get; set; }
       
        public ProjectTask() { }
        public ProjectTask(string name, string? description, DateTime? deadline)
        {
            this.Name = name;
            this.Description = description;
            this.Deadline = deadline;
            this.Priority = PriorityTaskEnum.Low;
        }
    }
}
