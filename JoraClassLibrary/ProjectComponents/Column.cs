using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.ObjectModel;

namespace JoraClassLibrary.ProjectComponents
{
    public class Column
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value.Length < 40 && value.Length > 0)
                {
                    name = value;
                }

            }
        }

        public ObservableCollection<ProjectTask> _tasks { get; set; } = new ObservableCollection<ProjectTask>();

        public void AddNewTask(ProjectTask task)
        {
            _tasks.Add(task);
        }
        public void ChangeTask(string taskName, ProjectTask changedTask)
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].Name == taskName)
                {
                    _tasks.Remove(_tasks[i]);
                    _tasks.Add(changedTask);
                }
            }
        }
        public bool LoadColumnTasksFromFile(string projectName, string colname)
        {
            if (!Directory.Exists(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"), "AllProjects"), projectName), "Columns"), colname))) throw new Exception();
            string[] pathes;
            pathes = Directory.GetFiles(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"), "AllProjects"), projectName), "Columns"), colname), "*.json");
            if (pathes == null) throw new Exception();
            foreach (string filePath in pathes)
            {
                string json = File.ReadAllText(filePath);
                Console.WriteLine(json);
                ProjectTask task = JsonSerializer.Deserialize<ProjectTask>(json);
                if (task != null)
                    _tasks.Add(task);

            }
            return true;

        }

        public ProjectTask GetTaskFromAColumn(string taskName)
        {
            foreach (ProjectTask task in _tasks)
            {
                if (task.Name == taskName)
                    return task;
            }
            return null;
        }


    }
}
