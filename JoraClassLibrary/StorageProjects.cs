using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JoraClassLibrary
{
    public class StorageProjects
    {

        List <string> _projects = new List <string> ();// названия для иконок проектов берутся из списка
        private string AllProjectsPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"), "AllProjects");

        private static StorageProjects _SP;
        private static readonly object _lock = new object();
        private StorageProjects() { }

        public static StorageProjects Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_SP == null)
                    {
                        _SP = new StorageProjects();
                    }
                    return _SP;
                }
            }
        }

        //МЕТОДЫ ДЛЯ УПРАВЛЕНИЯ ПРОЕКТОМ (НЕ СЧИТАЯ PROJECTINFO)

        public bool CreateNewProject(string name, string? description, DateTime? deadline, string login)//финальный метод для создания проекта
        {
            if (CurrentUser.Instance.currentUser != null)
            {
                if (!Directory.Exists(AllProjectsPath))
                    Directory.CreateDirectory(AllProjectsPath);
                if (Directory.Exists(Path.Combine(AllProjectsPath, name)))
                    return false;
                    Directory.CreateDirectory(Path.Combine(Path.Combine(AllProjectsPath, name)));
                Directory.CreateDirectory(Path.Combine(Path.Combine(AllProjectsPath, name), "Columns"));
                Directory.CreateDirectory(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, name), "Columns"), "To Do"));
                Directory.CreateDirectory(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, name), "Columns"), "In Progress"));
                Directory.CreateDirectory(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, name), "Columns"), "Done"));
                Project newProj = new Project();
                newProj.Name = name;
                newProj.SetProjectInfo(description, deadline);
                string projectInfoJson = JsonSerializer.Serialize(newProj.GetProjectInfo());
                File.WriteAllText(Path.Combine(Path.Combine(AllProjectsPath, name), "ProjectInfo.json"), projectInfoJson);
                newProj.AddUserToProject(login, RoleEnum.Leader);
                string teamJson = JsonSerializer.Serialize(newProj.GetTeam);
                File.WriteAllText(Path.Combine(Path.Combine(AllProjectsPath, name), "Team.json"), teamJson);
                CurrentUser.Instance.currentUser.AddProjectByNameToUsersProjectNamesList(newProj.Name);
                StorageUsers.Instance.UpdateCurrentUser();
                RefreshListProjects();
                return true;

            }
                return false;

        }
        public bool OpenProject(string projectName)// финальный метод для открытия проекта
        {    
            if (!Directory.Exists(Path.Combine(AllProjectsPath, projectName))) return false;
            Project projectFromFile = new Project();
            projectFromFile.Name = projectName;
            string projectInfoJson = File.ReadAllText(Path.Combine(Path.Combine(AllProjectsPath, projectName), "ProjectInfo.json"));
            ProjectInfo projInf = JsonSerializer.Deserialize<ProjectInfo>(projectInfoJson);
            projectFromFile.SetProjectInfo(projInf);
            List<ProjectUser> projectUsers = new List<ProjectUser>();
            projectUsers = JsonSerializer.Deserialize<List<ProjectUser>>((Path.Combine(Path.Combine(AllProjectsPath, projectName), "Team.json")));
            projectFromFile.SetTeam(projectUsers);
            string[] columnNames = Directory.GetDirectories(Path.Combine(AllProjectsPath, "Columns"));
            List<Column> columnsJson = new List<Column>();
            for (int i =0; i < columnNames.Length; i++)
            {
                Column columnJson = new Column();
                foreach (var filePath in Directory.GetFiles((Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, projectName), "Columns"), Path.GetFileName(columnNames[i]))), "*.json"))
                {
                    Task taskJson = JsonSerializer.Deserialize<Task>(filePath);
                    columnJson.AddNewTask(taskJson);
                }
                columnsJson.Add(columnJson);
            }
            projectFromFile.SetColumnsWithTasks(columnsJson);
            CurrentProject.Instance.SetProject(projectFromFile);
            return true;

        }

        public bool DeleteProject(string name)//удаление проекта в листе проектов каждого пользователя есть в GetExistingUserProjects()
        {
            if (!Directory.Exists(Path.Combine(AllProjectsPath, name)))
                return false;
            Directory.Delete(Path.Combine(AllProjectsPath, name));
            RefreshListProjects();
            return true;
        }

        /*public void SaveChangedProjectInfo()//?
        {
            File.WriteAllText(Path.Combine(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AllProjects"), CurrentProject.Instance.currentProject.Name), "ProjectInfo.json"), JsonSerializer.Serialize(CurrentProject.Instance.currentProject.GetProjectInfo()) );
        }

        public void SaveChangedTeam()//?
        {
            File.WriteAllText(Path.Combine(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AllProjects"), CurrentProject.Instance.currentProject.Name), "Team.json"), JsonSerializer.Serialize(CurrentProject.Instance.currentProject.GetTeam));
        }
        public void SaveChangedColumnName(string oldColumnName, string newColumnName)//?
        {
            if (newColumnName != oldColumnName)
            {
                string oldPath = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AllProjects"), CurrentProject.Instance.currentProject.Name), "Columns"), oldColumnName));
                string newPath = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AllProjects"), CurrentProject.Instance.currentProject.Name), "Columns"), newColumnName));
                Directory.Move(oldPath, newPath);
                File.Delete(oldPath);
            }
        
        }
        */

        public bool RefreshListProjects()
        {
                string[] projectDirectories = Directory.GetDirectories(AllProjectsPath);
            if (projectDirectories != null)
            {

                foreach (string d in projectDirectories)
                {
                    _projects.Add(d);
                }
                return true;
            }
            else return false; 
        }
        
        //МЕТОДЫ ДЛЯ УПРАВЛЕНИЯ PROJECTINFO (ВКЛЮЧАЯ НАЗВАНИЕ ПРОЕКТА)

        public bool ChangeProjectName(string newProjectName)// финальный метод для изменения названия проекта (current project)
        {
            if (CurrentProject.Instance.currentProject.Name != newProjectName)
            {

                Directory.CreateDirectory(Path.Combine(AllProjectsPath, newProjectName));
                Directory.Move(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), (Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name)));
                File.Move(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "ProjectInfo.json"), (Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name)));
                File.Move(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Team.json"), (Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name)));
            }
            return false;
        }
        internal bool ChangeCurrentProject_Info(string description, DateTime deadline)// готовый метод для изменения проджект инфо (когда запужен current project)
        {
            if (!File.Exists(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "ProjectInfo.json")))
                return false;
            ProjectInfo newProjectInfo = CurrentProject.Instance.currentProject.GetProjectInfo();
            newProjectInfo.description = description;
            newProjectInfo.deadline = deadline;
            string newProjectInfoJson = JsonSerializer.Serialize(newProjectInfo);
            File.WriteAllText(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "ProjectInfo.json"), newProjectInfoJson);
            return true;
        }
        internal ProjectInfo GetProjectInfoByProjectName(string projectName)// готовый метод для показания характеристик проетка в окне выбора проектов
        {
            return JsonSerializer.Deserialize<ProjectInfo>(Path.Combine(Path.Combine(AllProjectsPath, projectName), "ProjectInfo.json"));
        }

        //МЕТОДЫ ДЛЯ УПРАВЛЕНИЯ ПОЛЬЗОВАТЕЛЯМИ ПРОЕКТА

        
        

        public bool AddUserToCurrentProject(string login, RoleEnum role)// готовый метод для добавления пользователя в тиму проекта (у пользователя проект тоже появляется в списке проектов пользователя)
        {
            if (StorageUsers.Instance.FindUser(login)==null)
                return false;
            CurrentProject.Instance.currentProject.AddUserToProject(login, role);
            File.WriteAllText(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Team.json"), JsonSerializer.Serialize(CurrentProject.Instance.currentProject.GetTeam));
            return true;
        }
        public bool RemoveUserFromProject(string login)
        {
            CurrentProject.Instance.currentProject.RemoveUserFromProject(login);
            File.WriteAllText(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Team.json"), JsonSerializer.Serialize(CurrentProject.Instance.currentProject.GetTeam));
            if (StorageUsers.Instance.RemoveCurrentProjectFromUsersProjectList(login))
            return true;
            
            return false;
        }

        //МЕТОДЫ ДЛЯ УПРАВЛЕНИЯ КОЛОНКАМИ

        public bool CreateNewColumn(string columnName)
        {
            Directory.CreateDirectory(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"),columnName));
            CurrentProject.Instance.currentProject.CreateNewColumn(columnName);
            return true;
        }
        public bool ChangeColumnNameCurrentProject(string oldColumnName, string newColumnName)//финальный метод для смены названия колонки
        {
            if (newColumnName == oldColumnName)
                return false;

            Directory.CreateDirectory(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), newColumnName));
            Directory.Move(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), oldColumnName), (Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), newColumnName)));
            Directory.Delete(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), oldColumnName));
            CurrentProject.Instance.currentProject.ChangeColumnName(oldColumnName,newColumnName);
            return true;
        }

        public bool DeleteColumn(string columnName)
        {
            string[] tasks = Directory.GetFiles(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName));
            if (tasks.Length>0)
                return false;
            CurrentProject.Instance.currentProject.RemoveColumn(columnName);
            return true;
        }
        //МЕТОДЫ ДЛЯ УПРАВЛЕНИЯ ЗАДАЧАМИ

        public bool CreateNewTask(string columnName, string name, string description, DateTime deadline)// готовый метод для создания новых задач уже в нужной колонке
        {
            Task newTask = new Task(name, description, deadline);
            if (CurrentProject.Instance.currentProject.AddNewTask(columnName, newTask))
            {
                File.CreateText(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName), name+".json", JsonSerializer.Serialize(newTask)));
                return true;
            }
            else
                return false;
        }

        private bool ChangeTask(string columnName, string oldTaskName, string newTaskName, string newDescription, PriorityTaskEnum newPriority, DateTime newDeadline)
        {
            Task newTask = CurrentProject.Instance.currentProject.GetTask(oldTaskName, columnName);
            newTask.Name = newTaskName;
            newTask.Description = newDescription;
            newTask.Priority = newPriority;
            newTask.Deadline = newDeadline;
            if (newTask == CurrentProject.Instance.currentProject.GetTask(oldTaskName, columnName))
                return false;
            CurrentProject.Instance.currentProject.ChangeTask(oldTaskName, columnName, newTask);
            return true;
        }
        public bool ChangeTaskWithSave(string columnName, string oldTaskName, string newTaskName, string newDescription, PriorityTaskEnum newPriority, DateTime newDeadline)// готовфй метод для изменения таска 
        {
            if (!File.Exists(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName), oldTaskName)))
                return false;
            if (newTaskName == oldTaskName)
            {
                string changedTaskJson = JsonSerializer.Serialize(CurrentProject.Instance.currentProject.GetTask(oldTaskName, columnName));
                File.WriteAllText(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName), oldTaskName), changedTaskJson);
                return true;
            }
            ChangeTask(columnName, oldTaskName, newTaskName, newDescription, newPriority, newDeadline);
            string changedTaskJsonWN = JsonSerializer.Serialize(CurrentProject.Instance.currentProject.GetTask(newTaskName, columnName));
            File.WriteAllText(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName), newTaskName), changedTaskJsonWN);
            File.Delete(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName), oldTaskName));
            return true;
        }
        public void SaveMovedTask(string oldColumnName, string newColumnName, string taskName)//готовый метод для смены колонки задания
        {
            string oldPath = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), oldColumnName), taskName));
            string newPath = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), newColumnName), taskName));
            File.Move(oldPath, newPath);
        }
        public bool DeleteTask(string taskName, string columnName)//готовый метод для удаления задачи(как в currentproject, так и в файлах)
        {
            CurrentProject.Instance.currentProject.DeleteTask(taskName, columnName);
            File.Delete((Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName), taskName)));
            return true;
        }
       
    }
}
