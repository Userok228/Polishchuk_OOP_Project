using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace JoraClassLibrary
{
    internal class Column
    {
        private string name;
        public string Name
        {
            get { return name; }
            set {
                if (value.Length < 40 && value.Length > 0)
                {
                    name = value;
                }
                    
            }
        }
        //подгрузка задачь из файла в лист колонки
        public List<Task> _tasks = new List<Task>();
        
        public void AddTask(string projectName, string name, string description, DateTime deadline) 
        {
           Task newtask = new Task(name,description,deadline);
            string pathColumn = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"), "AllProjects")), projectName), "_columns"), this.name);
            string json = JsonSerializer.Serialize(newtask);
            File.WriteAllText(pathColumn, json);

        }
        public void AddNewTask(Task task)
        {
            _tasks.Add(task);
        }
        public void ChangeTask(string taskName, Task changedTask)
        {
            foreach (Task t in _tasks)
            {
                if (t.Name == taskName)
                {
                    _tasks.Remove(t);
                    _tasks.Add(changedTask);
                    return;

                }
            }
        }
        public bool RefreshColumnTasks(string projectName)
        {
            if (!File.Exists(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"), "AllProjects")), projectName), "Columns"), name))) return false;
            foreach (var filePath in Directory.GetFiles((Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"), "AllProjects")), projectName), "Columns"), name)), "*.json"))
            {
                string json = File.ReadAllText(filePath);
                
                _tasks.Add(JsonSerializer.Deserialize<Task>(json));
            }
            return true;

        }

        public Task GetTaskFromAColumn(string taskName)
        {
            return _tasks.FirstOrDefault(t => t.Name == taskName);
        }

    }
}
