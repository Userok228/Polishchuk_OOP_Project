using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JoraClassLibrary
{
    internal sealed class Board
    {
        // подгрузка колонок из файла в листы
        public List <Column> _columns = new List <Column> ();
        private string AllProjectsPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"), "AllProjects");

        public bool AddColumn(string projectName,string name) 
        {
            if (Directory.Exists(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, projectName), "Columns"), name)))
                return false;

            Column newcolumn = new Column ();
            newcolumn.Name = name;
            Directory.CreateDirectory(Path.Combine(Path.Combine(Path.Combine(AllProjectsPath, projectName), "Columns"), name));
            _columns.Add(newcolumn);

            return true;
        }
        public void CreateNewColumn(string name)
        {
            _columns.Add(new Column { Name = name });   
        }
        public void SetColumns(List<Column> col)
        {
            _columns = col;
        }
        public bool LoadListColumnsWithTasks_FromFile(string name)// не нужно?
        {
            string[] directories = Directory.GetDirectories(Path.Combine(Path.Combine(AllProjectsPath, name), "Columns"));
            if (directories != null)
            {

                foreach (string d in directories)
                {
                    Column column = new Column();
                    column.Name = Path.GetFileName(d);
                    column.RefreshColumnTasks(name);
                    _columns.Add(column);
                }
                return true;
            }
            else return false;
        }
        internal List <string> GetColumnsNames()
        {
            List<string> names = new List<string>();
           foreach(Column c in _columns)
            {
                names.Add(c.Name);
            }
            return names;
        }
        public bool ChangeColumnName(string oldName, string newName)
        {
            foreach (Column column in _columns) 
            {
                if (oldName == column.Name) 
                {
                column.Name = newName;
                    return true;
                }
            }
            return false;
        }
        public bool RemoveColumn(string name)
        {
            foreach (Column column in _columns)
            {
                if (name == column.Name)
                {
                   _columns.Remove(column);
                    return true;
                }
            }
            return false;
        }

        public bool AddNewTask(string columnName, Task newTask)//????????
        {
            foreach(Column column in _columns)
            {
                if (columnName == column.Name)
                {
                    column.AddNewTask(newTask);
                    return true;
                }
            }
            return false;
        }
        public Task GetTask(string taskName, string columnName)
        {
            foreach(Column column in _columns)
            {
                if (column.Name == columnName)
                {
                   return column.GetTaskFromAColumn(taskName);
                }
            }
            return null;
        }
        public void ChangeTask(string taskName, string columnName, Task changedTask)
        {

            foreach (Column column in _columns)
            {
                if (column.Name == columnName)
                {
                    column.ChangeTask(taskName, changedTask);
                }
            }
        }
        public void DeleteTask(string taskName, string columnName) 
        {
            Task task;
            foreach (var column in _columns)
            {
                if (column.Name == columnName)
                foreach (Task t in column._tasks)
                {              
                    if (t.Name == taskName)
                    {
                        column._tasks.Remove(t);
                            return;
                    }
                }
           
            }
        }

    }
}
