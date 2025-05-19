using System.Globalization;

namespace JoraClassLibrary
{
    public class Task
    {
        private string name;
        private string description;
        private PriorityTaskEnum priority;//enum
        private DateTime creationDate;
        private DateTime deadline;
        public string Name 
        {
            get { return name; } 
            internal set // может работать некоректно
            {
                    name = value;
            }
        }
        public string Description
        {
            get { return description; }
            internal set
            {
                    description = value;
            }
        }    
        public PriorityTaskEnum Priority 
        {
            get { return priority; }
            internal set
            {
                    priority = value;
            }
        }
        public DateTime CreationDate
        {
            get { return creationDate; }
            internal set
            {
             creationDate = DateTime.Now;
            }
        }
        public DateTime Deadline
        {
            get { return deadline; }
            internal set
            {
                    deadline = value;
            }
            
        }

        public Task(string name, string description, DateTime deadline)
        {
            this.Name = name;
            this.Description = description;
            this.Deadline = deadline;
            this.Priority = PriorityTaskEnum.Low;
            this.CreationDate = DateTime.Now;
        }
    }
}
