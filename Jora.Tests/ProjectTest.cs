using JoraClassLibrary;
using JoraClassLibrary.Enums;
using JoraClassLibrary.ProjectComponents;
using JoraClassLibrary.User.User;
using System.Globalization;
using System.Xml.Linq;

namespace Jora.Tests
{
    public class ProjectTest
    {
        public string UsersPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"), "UsersJ.json");
        private string AllProjectsPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"), "AllProjects");

        [Theory]
        [InlineData("TestSucces1", "Description", "9999-03-03")]
        [InlineData("TestSucces2", "Description", null)]
        [InlineData("TestSucces3", null, "9999-03-03")]
        [InlineData("TestSucces4", null, null)]
        [InlineData("TestFail", "Description", "2024-03-03")]
        [InlineData("", "Description", "9999-03-03")]

        public void CreationProjectTest(string name, string? desceription, string? deadline)
        {
            if(Directory.Exists(AllProjectsPath))
            Directory.Delete(AllProjectsPath,true);
            if(File.Exists(UsersPath))
            File.Delete(UsersPath);
            bool res;
            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            
            StorageUsers.Instance.LogIn("login","password");
            if (deadline != null)
            {
                DateTime dead = DateTime.Parse(deadline);
                res = StorageProjects.Instance.CreateNewProject(name, desceription, dead);
            }
            else { res = StorageProjects.Instance.CreateNewProject(name, desceription, null); }
            if (name != "TestFail" && name != "")
                Assert.True(res);
            else Assert.False(res);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void OpenProject(bool res)
        {
            if (Directory.Exists(AllProjectsPath))
                Directory.Delete(AllProjectsPath, true);
            if (File.Exists(UsersPath))
                File.Delete(UsersPath);
            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.LogIn("login", "password");
            DateTime dead = DateTime.Parse("9999-03-03");
            StorageProjects.Instance.CreateNewProject("TestSucces", "Description", dead);
            if (res)
                Assert.True(StorageProjects.Instance.OpenProject("TestSucces"));
            else
                Assert.False(StorageProjects.Instance.OpenProject("TestFail"));

        }
        [Theory]
        [InlineData("TestSucces")]
        [InlineData("TestFail")]
        public void DeleteProject(string name)
        {
            if (Directory.Exists(AllProjectsPath))
                Directory.Delete(AllProjectsPath, true);
            if (File.Exists(UsersPath))
                File.Delete(UsersPath);
            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.LogIn("login", "password");
            DateTime dead = DateTime.Parse("9999-03-03");
            StorageProjects.Instance.CreateNewProject("TestSucces", "Description", dead);
            if (name == "TestSucces")
                Assert.True(StorageProjects.Instance.DeleteProject(name));
            else Assert.False(StorageProjects.Instance.DeleteProject(name));
        }

        [Theory]
        [InlineData("new description", "9998-09-09")]
        [InlineData(null, null)]
        public void ChangeProjectInfo(string? desc, string? deadline)
        {
            if (Directory.Exists(AllProjectsPath))
                Directory.Delete(AllProjectsPath, true);
            if (File.Exists(UsersPath))
                File.Delete(UsersPath);
            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.LogIn("login", "password");
            DateTime dead = DateTime.Parse("9999-03-03");
            StorageProjects.Instance.CreateNewProject("TestSucces", "Description", dead);
            StorageProjects.Instance.OpenProject("TestSucces");
            if (deadline != null)
            {
            dead = DateTime.Parse(deadline) ;
            Assert.True(StorageProjects.Instance.ChangeCurrentProject_Info(desc, dead));
            }
            else
            {
                Assert.True(StorageProjects.Instance.ChangeCurrentProject_Info(desc, null));
            }
        }
        [Theory]
        [InlineData("ColumnName")]
        [InlineData("")]
        public void CreateColumn(string name)
        {
            if (Directory.Exists(AllProjectsPath))
                Directory.Delete(AllProjectsPath, true);
            if (File.Exists(UsersPath))
                File.Delete(UsersPath);
            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.LogIn("login", "password");
            DateTime dead = DateTime.Parse("9999-03-03");
            StorageProjects.Instance.CreateNewProject("TestSucces", "Description", dead);
            StorageProjects.Instance.OpenProject("TestSucces");
            Assert.True(StorageProjects.Instance.CreateNewColumn(name));

        }
        [Theory]
        [InlineData("NewColumnName")]
        [InlineData("ColumnColumnColumnColumnColumnColumnColumn")]
        [InlineData("")]

        public void ChangeColumn(string newColName)
        {
            if (Directory.Exists(AllProjectsPath))
                Directory.Delete(AllProjectsPath, true);
            if (File.Exists(UsersPath))
                File.Delete(UsersPath);
            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.LogIn("login", "password");
            DateTime dead = DateTime.Parse("9999-03-03");
            StorageProjects.Instance.CreateNewProject("TestSucces", "Description", dead);
            StorageProjects.Instance.OpenProject("TestSucces");
            if (newColName == "NewColumnName")
                Assert.True(StorageProjects.Instance.ChangeColumnNameCurrentProject("Done", newColName));
            else
                Assert.False(StorageProjects.Instance.ChangeColumnNameCurrentProject("Done", newColName));
        }
        [Theory]
        [InlineData("Done")]
        [InlineData("ColumnName")]
        public void DeleteColumn(string colName)
        {
            if (Directory.Exists(AllProjectsPath))
                Directory.Delete(AllProjectsPath, true);
            if (File.Exists(UsersPath))
                File.Delete(UsersPath);
            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.LogIn("login", "password");
            DateTime dead = DateTime.Parse("9999-03-03");
            StorageProjects.Instance.CreateNewProject("TestSucces", "Description", dead);
            StorageProjects.Instance.OpenProject("TestSucces");
            if (colName == "Done")
                Assert.True(StorageProjects.Instance.DeleteColumn(colName));
            else
                Assert.False(StorageProjects.Instance.DeleteColumn(colName));
        }
        [Theory]
        [InlineData("task","description", "09.09.2999")]
        [InlineData("task","description", null)]
        [InlineData("task", null, "09.09.9999")]
        [InlineData("task", "", null)]
        [InlineData("failure", null, "deadline")]
        [InlineData("failure", null, "2024.10.05")]

        public void CreateTask(string taskName, string? description, string? deadline)
        {
            if (Directory.Exists(AllProjectsPath))
                Directory.Delete(AllProjectsPath, true);
            if (File.Exists(UsersPath))
                File.Delete(UsersPath);
            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.LogIn("login", "password");
            DateTime dead = DateTime.Parse("2999-03-03");
            StorageProjects.Instance.CreateNewProject("TestSucces", "Description", dead);
            StorageProjects.Instance.OpenProject("TestSucces");
            if (deadline != null)
            {
                DateTime deadl;
                DateTime.TryParseExact(deadline, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out deadl);
                if (taskName == "task")
                    Assert.True(StorageProjects.Instance.CreateNewTask("Done", taskName, description, deadl));
                else
                    Assert.False(StorageProjects.Instance.CreateNewTask("Done", taskName, description, deadl));
            }
            else
            {
                if (taskName == "task")
                    Assert.True(StorageProjects.Instance.CreateNewTask("Done", taskName, description, null));
                else
                Assert.False(StorageProjects.Instance.CreateNewTask("Done", taskName, description, null));
            }
            
        }
        [Theory]
        [InlineData("Done","In Progress")]
        [InlineData("Done","Done")]

        public void MoveTask(string oldColumn, string newColumn)
        {
            if (Directory.Exists(AllProjectsPath))
                Directory.Delete(AllProjectsPath, true);
            if (File.Exists(UsersPath))
                File.Delete(UsersPath);
            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.LogIn("login", "password");
            DateTime dead = DateTime.Parse("9999-03-03");
            StorageProjects.Instance.CreateNewProject("TestSucces", "Description", dead);
            StorageProjects.Instance.OpenProject("TestSucces");
            StorageProjects.Instance.CreateNewTask("Done", "testTask", null, null);
            StorageProjects.Instance.SaveMovedTask(oldColumn, newColumn, "testTask");
        }
        [Theory]
        [InlineData("newTaskName", "newDescritpion", PriorityTaskEnum.High, "9998.09.09")]
        [InlineData("testTask", "newDescritpion", PriorityTaskEnum.Medium, "9998.09.09")]
        [InlineData("newTaskName", "newDescritpion", PriorityTaskEnum.Low, "9998.09.09")]
        [InlineData("newTaskName", null, PriorityTaskEnum.Low, null)]
        [InlineData("testTask", null, PriorityTaskEnum.High, null)]
        [InlineData("testTask", null, PriorityTaskEnum.Low, null)]
        [InlineData("","newDescript", PriorityTaskEnum.Low, null)]
        public void ChangeTask(string newTaskName, string? newDescription, PriorityTaskEnum newPriority, string? deadline)
        {
            if (Directory.Exists(AllProjectsPath))
                Directory.Delete(AllProjectsPath, true);
            if (File.Exists(UsersPath))
                File.Delete(UsersPath);
            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.LogIn("login", "password");
            DateTime dead = DateTime.Parse("9999-03-03");
            StorageProjects.Instance.CreateNewProject("TestSucces", "Description", dead);
            StorageProjects.Instance.OpenProject("TestSucces");
            StorageProjects.Instance.CreateNewTask("Done", "testTask", null, null);
            if (newTaskName.Length == 0)
                Assert.False(StorageProjects.Instance.ChangeTaskWithSave("Done", "testTask", newTaskName, newDescription, newPriority, null));
            if (newTaskName == "newTaskName" || newTaskName == "testTask")
            {
                if (deadline != null)
                {
                    DateTime.TryParseExact(deadline, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dead);
                    Assert.True(StorageProjects.Instance.ChangeTaskWithSave("Done", "testTask", newTaskName, newDescription, newPriority, dead));
                }
                else Assert.True(StorageProjects.Instance.ChangeTaskWithSave("Done", "testTask", newTaskName, newDescription, newPriority, null));

            }
            else Assert.False(StorageProjects.Instance.ChangeTaskWithSave("Done", "testTask", newTaskName, newDescription, newPriority, null));
        }
        [Theory]
        [InlineData("task")]
        [InlineData("fail")]

        public void TaskDelete(string taskName)
        {
            if (Directory.Exists(AllProjectsPath))
                Directory.Delete(AllProjectsPath, true);
            if (File.Exists(UsersPath))
                File.Delete(UsersPath);
            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.LogIn("login", "password");
            DateTime dead = DateTime.Parse("9999-03-03");
            StorageProjects.Instance.CreateNewProject("TestSucces", "Description", dead);
            StorageProjects.Instance.OpenProject("TestSucces");
            StorageProjects.Instance.CreateNewTask("Done", taskName, null, null);
            if (taskName == "task")
                Assert.True(StorageProjects.Instance.DeleteTask("task", "Done"));
            else Assert.False(StorageProjects.Instance.DeleteTask("task", "Done"));
        }
        [Theory]
        [InlineData (true)]
        [InlineData (false)]
        public void InviteMember(bool res)
        {
            if (Directory.Exists(AllProjectsPath))
                Directory.Delete(AllProjectsPath, true);
            if (File.Exists(UsersPath))
                File.Delete(UsersPath);
            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.CreationNewUser("member", "password", "password", "user", "@ema");
            StorageUsers.Instance.LogIn("login", "password");
            DateTime dead = DateTime.Parse("9999-03-03");
            StorageProjects.Instance.CreateNewProject("TestSucces", "Description", dead);
            StorageProjects.Instance.OpenProject("TestSucces");
            if (res)
               Assert.True(StorageProjects.Instance.AddUserToCurrentProject("member", RoleEnum.Member));
            else
                Assert.False(StorageProjects.Instance.AddUserToCurrentProject("memb", RoleEnum.Member));

        }
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeleteMember(bool res)
        {
            if (Directory.Exists(AllProjectsPath))
                Directory.Delete(AllProjectsPath, true);
            if (File.Exists(UsersPath))
                File.Delete(UsersPath);
            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.CreationNewUser("member", "password", "password", "user", "@ema");
            StorageUsers.Instance.LogIn("login", "password");
            DateTime dead = DateTime.Parse("9999-03-03");
            StorageProjects.Instance.CreateNewProject("TestSucces", "Description", dead);
            StorageProjects.Instance.OpenProject("TestSucces");
                Assert.True(StorageProjects.Instance.AddUserToCurrentProject("member", RoleEnum.Member));
            if (res)
            {
                Assert.True(StorageProjects.Instance.RemoveUserFromProject("member"));
            }
            else
            Assert.False(StorageProjects.Instance.RemoveUserFromProject("login"));

        }
    }
}