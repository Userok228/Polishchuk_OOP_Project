using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace JoraClassLibrary
{
    public class User
    {
        private string AllProjectsPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"), "AllProjects");
        public string login { get; private set; }
        public string password { get; private set; }
        public string username { get; private set; }
        public string email { get; private set; }

        public List<string> _projectNames { get; set; } = new List<string>();

        public User(string login, string password, string username, string email)
        {
            this.login = login;
            this.password = password;
            this.username = username;
            this.email = email;
        }

       

        
        public List<string> GetProjectNames()
        {
            return _projectNames;
        }
        public void RemoveProjectfromProjectNamesList_ByName(string projName)
        {
            _projectNames.Remove(projName);
        }
        public void ChangeUserName(string name)
        {
           this.username = name;
        }

        public void ChangePassword(string newpassword)
        {

                password = newpassword;
        }

        public void ChangeEmail(string email)
        {
            this.email = email;
        }
        public bool AddProjectUsersProjectNamesList()
        {
            if (Directory.Exists(Path.Combine(AllProjectsPath, CurrentProject.Instance.currentProject.Name)))
            {
                _projectNames.Add(CurrentProject.Instance.currentProject.Name); return true;
            }
            else return false;
        }
        internal bool AddProjectByNameToUsersProjectNamesList(string projectName)
        {
            if (Directory.Exists(Path.Combine(Path.Combine(AllProjectsPath, projectName))))
            {
                _projectNames.Add(projectName); 
                
                return true;
            }
            return false;
        }



    }
}
