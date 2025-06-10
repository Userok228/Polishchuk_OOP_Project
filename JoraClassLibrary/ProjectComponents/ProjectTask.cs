using System.Globalization;
using JoraClassLibrary.Enums;

namespace JoraClassLibrary.ProjectComponents
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
            Name = name;
            Description = description;
            Deadline = deadline;
            Priority = PriorityTaskEnum.Low;
        }
        public ProjectTask(string name, string? description, DateTime? deadline, PriorityTaskEnum priority)
        {
            Name = name;
            Description = description;
            Deadline = deadline;
            Priority = priority;
        }
    }
}
