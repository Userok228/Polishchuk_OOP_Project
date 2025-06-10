using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoraClassLibrary.Enums;

namespace JoraClassLibrary.ProjectComponents
{
    public class ProjectUser
    {
        public string login { get; set; }
        public string username { get; set; }
        public RoleEnum role { get; set; }



        public ProjectUser() { }
        public ProjectUser(string login, string name, RoleEnum role)
        {
            this.login = login;
            username = name;
            this.role = role;
        }
        public string GetLogin()
        {
            return login;
        }
    }
}
