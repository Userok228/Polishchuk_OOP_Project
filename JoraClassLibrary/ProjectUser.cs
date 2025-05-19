using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoraClassLibrary
{
    public class ProjectUser
    {
        public string userLogin { get; set; }
        public RoleEnum role { get; set; }

        public ProjectUser(string user, RoleEnum role)
        {
            this.userLogin = user;
            this.role = role;
        }
        public string GetLogin()
        {
            return userLogin;
        }
    }
}
