using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoraClassLibrary
{
    public class Project
    {
            string name;
            ProjectInfo info = new ProjectInfo();
            List<ProjectUser> _team = new List<ProjectUser>();
            Board board = new Board();
        public string Name
        {
            get { return name; }
            internal set
            {
                name = value;
            }
        }

        internal List<ProjectUser> GetTeam { get { return _team; } }

        internal void SetProjectInfo(ProjectInfo inf)
        {
            info = inf;
        }
        internal void SetProjectInfo(string? description, DateTime? deadline)
        {
            info.SetProjetcInfo(description, deadline);
        }
        internal ProjectInfo GetProjectInfo()
        {
            return info;
        }

        internal void SetName(string newProjectName)
        {
            name = newProjectName;
        }
       
        
        internal void SetTeam(List<ProjectUser> loadUsers)
        {
            _team = loadUsers;
        }
        internal void CreateNewColumn(string columnName)
        {
            board.CreateNewColumn(columnName);
        }
        internal void ChangeColumnName(string oldColumnName, string newColumnName)
        {
            board.ChangeColumnName(oldColumnName, newColumnName);
        }
        internal void RemoveColumn(string columnName) 
        { 
        board.RemoveColumn(columnName);
        }
        internal List<string> GetColumnsNames()
        {
            return board.GetColumnsNames();
        }
        internal List<Column> GetColumns()
        {
            return board._columns;
        }
        public string GetColumnNameByTask(string taskName)
        {
            return board.GetColumnNameByTask(taskName);
        }
        internal void SetColumnsWithTasks(string name)
        {
            board.LoadListColumnsWithTasks_FromFile(name);
        }
        internal bool AddNewTask(string columnName, ProjectTask task)
        {
            if (board.AddNewTask(columnName, task)) return true;
            return false;
        }
        public void ChangeTask(string oldTaskName, string columnName, ProjectTask newTesk)
        {
            board.ChangeTask(oldTaskName, columnName, newTesk);

        }
        internal ProjectTask GetTask(string taskName, string columnName)
        {
            return board.GetTask(taskName, columnName);
        }
        internal void MoveTaskInCurrentProject(string taskName, string oldColumn, string newColumn)
        {
            board.MoveTaskInCurrentProject(taskName, oldColumn, newColumn);
        }
        internal void DeleteTask(string taskName, string columnName)
        {
            board.DeleteTask(taskName, columnName);
        }
        public bool AddUserToProject(string login, RoleEnum role) 
        {
                if (StorageUsers.Instance.FindUser(login) != null)
                {
                    ProjectUser newuser = new ProjectUser(login, role);
                    _team.Add(newuser);
                    return true;
                }
            return false;
           
        } 
        public void RemoveUserFromProject(string login)
        {
            foreach(ProjectUser u in _team) 
            {
            if(u.GetLogin() == login)
            _team.Remove(u);  
            }
        }

    }
}