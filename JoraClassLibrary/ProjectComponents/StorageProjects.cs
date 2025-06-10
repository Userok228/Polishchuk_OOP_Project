using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using JoraClassLibrary.Enums;
using JoraClassLibrary.User.User;

namespace JoraClassLibrary.ProjectComponents
{
    public class StorageProjects
    {

        private List<string> _projects = new List<string>();// названия для иконок проектов берутся из списка
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
                newProj.AddUserToProject(CurrentUser.Instance.currentUser.login, CurrentUser.Instance.currentUser.username, RoleEnum.Leader);
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
            string projectUsersJson = File.ReadAllText(Path.Combine(Path.Combine(AllProjectsPath, projectName), "Team.json"));
            List<ProjectUser> projectUsers = JsonSerializer.Deserialize<List<ProjectUser>>(projectUsersJson);
            projectFromFile.SetTeam(projectUsers);
            string[] columnsPath = Directory.GetDirectories(Path.Combine(Path.Combine(AllProjectsPath, projectName), "Columns"));
            string[] columnNames = Array.ConvertAll(columnsPath, Path.GetFileName);
            List<Column> columnsJson = new List<Column>();
            projectFromFile.SetColumnsWithTasks(projectName);
            CurrentProject.Instance.SetProject(projectFromFile);
            return true;

        }

        public List<string> GetSummaryInfo()
        {
            List<string> columns = new List<string>();
            foreach (string c in CurrentProject.Instance.currentProject.GetColumnsNames())
            {
                int i = Directory.GetFiles(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), c), "*.json").Length;
                columns.Add(c + ": " + i);
            }
            return columns;

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
                Directory.Move(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), Path.Combine(AllProjectsPath, newProjectName));// тут ошибка cannot create because a file or directory with the same name already exists.'
                File.Move(Path.Combine(Path.Combine(AllProjectsPath, newProjectName), "ProjectInfo.json"), Path.Combine(Path.Combine(AllProjectsPath, newProjectName), "ProjectInfo.json"));
                File.Move(Path.Combine(Path.Combine(AllProjectsPath, newProjectName), "Team.json"), Path.Combine(Path.Combine(AllProjectsPath, newProjectName), "Team.json"));
                
                
                return true;
            }
            else
            return false;
        }
        public bool ChangeCurrentProject_Info(string description, DateTime? deadline)// готовый метод для изменения проджект инфо (когда запужен current project)
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
        public ProjectInfo GetProjectInfoByProjectName(string projectName)// готовый метод для показания характеристик проетка в окне выбора проектов
        {
            string text = File.ReadAllText(Path.Combine(Path.Combine(AllProjectsPath, projectName), "ProjectInfo.json"));
            return JsonSerializer.Deserialize<ProjectInfo>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public bool SaveAdver(string adver)
        {
            if (adver.Length < 2500)
            {
                string json = JsonSerializer.Serialize(adver);
                File.WriteAllText(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Advertisements.json"), json);
                return true;
            }
            return false;
        }

        public string GetAdver()
        {
            if (File.Exists(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Advertisements.json")))
            {
                return JsonSerializer.Deserialize<string>(File.ReadAllText(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Advertisements.json")));
            }
            else return string.Empty;
        }
        //МЕТОДЫ ДЛЯ УПРАВЛЕНИЯ ПОЛЬЗОВАТЕЛЯМИ ПРОЕКТА


        public RoleEnum GetCurrentRole()
        {
            foreach (ProjectUser u in CurrentProject.Instance.currentProject.GetTeam)
            {
                if (CurrentUser.Instance.currentUser.login == u.login)
                    return u.role;
            }

            throw new Exception("User not found");
        }

        public bool AddUserToCurrentProject(string login, RoleEnum role)// готовый метод для добавления пользователя в тиму проекта (у пользователя проект тоже появляется в списке проектов пользователя)
        {
            if (StorageUsers.Instance.FindUser(login) == null)
                return false;
            CurrentProject.Instance.currentProject.AddUserToProject(login, StorageUsers.Instance.FindUser(login).username, role);
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

        public bool UserNotRemovedFromProjectCheck(string projName)
        {
            string usersJson = File.ReadAllText(Path.Combine(Path.Combine(AllProjectsPath, projName), "Team.json"));
            List<ProjectUser> team = JsonSerializer.Deserialize<List<ProjectUser>>(usersJson);
            foreach (ProjectUser u in team)
            {
                if (u.login == CurrentUser.Instance.currentUser.login)
                    return true;
            }
            return false;
        }

        //МЕТОДЫ ДЛЯ УПРАВЛЕНИЯ КОЛОНКАМИ

        public bool CreateNewColumn(string columnName)
        {
            Directory.CreateDirectory(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName));
            CurrentProject.Instance.currentProject.CreateNewColumn(columnName);
            return true;
        }
        public bool ChangeColumnNameCurrentProject(string oldColumnName, string newColumnName)//финальный метод для смены названия колонки
        {
            if (newColumnName == oldColumnName)
                return true;
            if (newColumnName.Length > 40)
                return false;
            if (Directory.Exists(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), newColumnName)))
                return false;
            // Directory.CreateDirectory(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), newColumnName));
            Directory.Move(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), oldColumnName), Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), newColumnName));

            CurrentProject.Instance.currentProject.ChangeColumnName(oldColumnName, newColumnName);
            return true;
        }
        public List<Column> GetColumns()
        {
            return CurrentProject.Instance.currentProject.GetColumns();
        }
        public bool DeleteColumn(string columnName)
        {
            string[] tasks = Directory.GetFiles(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName));
            if (tasks.Length != 0)
                return false;
            CurrentProject.Instance.currentProject.RemoveColumn(columnName);
            Directory.Delete(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName));
            return true;
        }
        //МЕТОДЫ ДЛЯ УПРАВЛЕНИЯ ЗАДАЧАМИ

        public bool CreateNewTask(string columnName, string name, string? description, DateTime? deadline)// готовый метод для создания новых задач уже в нужной колонке
        {
            ProjectTask newTask = new ProjectTask(name, description, deadline);
            if (CurrentProject.Instance.currentProject.AddNewTask(columnName, newTask))
            {
                File.WriteAllText(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName), name + ".json"), JsonSerializer.Serialize(newTask));
                return true;
            }
            else
                return false;
        }

        private bool ChangeTask(string columnName, string oldTaskName, string newTaskName, string newDescription, PriorityTaskEnum newPriority, DateTime? newDeadline)
        {
            ProjectTask newTask = CurrentProject.Instance.currentProject.GetTask(newTaskName, columnName);
            newTask.Name = newTaskName;
            newTask.Description = newDescription;
            newTask.Priority = newPriority;
            newTask.Deadline = newDeadline;
            if (newTask == CurrentProject.Instance.currentProject.GetTask(oldTaskName, columnName))
                return false;
            CurrentProject.Instance.currentProject.ChangeTask(oldTaskName, columnName, newTask);
            // File.WriteAllText(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName), newTaskName + ".json"), JsonSerializer.Serialize(newTask));
            return true;
        }
        public bool ChangeTaskWithSave(string columnName, string oldTaskName, string newTaskName, string newDescription, PriorityTaskEnum newPriority, DateTime? newDeadline)// готовфй метод для изменения таска 
        {
            if (!File.Exists(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName), oldTaskName + ".json")))
                return false;
            if (!ChangeTask(columnName, oldTaskName, newTaskName, newDescription, newPriority, newDeadline)) { return false; }
            string changedTaskJson = JsonSerializer.Serialize(CurrentProject.Instance.currentProject.GetTask(newTaskName, columnName));//oldtaskname
            File.Delete(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName), oldTaskName + ".json"));
            File.WriteAllText(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName), newTaskName + ".json"), changedTaskJson);


            return true;


        }
        public void SaveMovedTask(string oldColumnName, string newColumnName, string taskName)//готовый метод для смены колонки задания
        {

            string oldPath = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), oldColumnName), taskName + ".json"));
            string newPath = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), newColumnName), taskName + ".json"));
            CurrentProject.Instance.currentProject.MoveTaskInCurrentProject(taskName, oldColumnName, newColumnName);
            File.Move(oldPath, newPath);
        }
        public bool DeleteTask(string taskName, string columnName)//готовый метод для удаления задачи(как в currentproject, так и в файлах)
        {
            CurrentProject.Instance.currentProject.DeleteTask(taskName, columnName);
            File.Delete(Path.Combine(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name), "Columns"), columnName), taskName + ".json"));
            return true;
        }

    }
}
