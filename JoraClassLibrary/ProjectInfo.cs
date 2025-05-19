using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoraClassLibrary
{
    internal class ProjectInfo
    {
        public string ?description;
        public DateTime creationDate;
        public DateTime? deadline;

        public void SetProjetcInfo(string? desc, DateTime? dead)
        {
            Deadline = dead;
            Description = desc;
        } 

        public string? Description
        {
            get { return description; }
            private set
            {
                description = value;
            }
        }
        public DateTime CreationDate
        {
            get { return creationDate; }
            private set
            {
                creationDate = DateTime.Now;
            }
        }
        public DateTime? Deadline
        {
            get { return deadline; }
            private set
            {
                deadline = value;
            }

        }
    }
}
