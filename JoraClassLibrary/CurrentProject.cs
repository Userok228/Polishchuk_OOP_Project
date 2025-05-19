using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoraClassLibrary
{
    public class CurrentProject
    {
        private static CurrentProject _CP;
        private static readonly object _lock = new object();
        public Project currentProject { get; private set; }
        private CurrentProject() { }

        public static CurrentProject Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_CP == null)
                    {
                        _CP = new CurrentProject();
                    }
                    return _CP;
                }
            }
        }
        public void SetProject(Project projectFromFile)
        {
            currentProject = projectFromFile;
        }

        public string GetName()
        {
            return currentProject.Name;
        }
    }
}
