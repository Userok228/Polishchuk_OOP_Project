using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoraClassLibrary.ProjectComponents
{
    public class ProjectInfo
    {
        public string? description { get; set; }
        public DateTime? deadline { get; set; }
        public DateTime creationDate { get; set; } = DateTime.Now;

        public void SetProjetcInfo(string? desc, DateTime? dead)
        {
            deadline = dead;
            description = desc;
        }

    }
}
