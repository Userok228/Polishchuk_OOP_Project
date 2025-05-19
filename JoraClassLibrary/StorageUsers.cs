using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace JoraClassLibrary
{
    public class StorageUsers
    {
        public string usersPath =Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Jora"), "UsersJ.json");
        private string AllProjectsPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"), "AllProjects");

        private static StorageUsers _SU;
        private static readonly object _lock = new object();
        public List<User> _users = new List<User>();
        bool firstRefreshListUsers = false;
        private StorageUsers() { }

        public static StorageUsers Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_SU == null)
                    {
                        _SU = new StorageUsers();
                    }
                    return _SU;
                }
            }
        }

       private void RefreshListUsers()
        {
            string json = File.ReadAllText(usersPath);
            if (JsonSerializer.Deserialize<List<User>>(json) != null)
            {
                _users = JsonSerializer.Deserialize<List<User>>(json);
            }
            else _users = new List<User>();
        }
        private void RefreshJsonUsers() 
        {
            string updatedUsersJ = JsonSerializer.Serialize(_users);
            File.WriteAllText(usersPath, updatedUsersJ);
        }
        public bool CreationNewUser(string login, string password, string repeatPassword, string username, string email)//финальный метод для создания и регистрации ползователя
        {
            bool fileJustCreated = false;
            if (!File.Exists(usersPath))
            {
                if(!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora")))
                {
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"));
                }
                using (File.Create(usersPath)) { };
                fileJustCreated = true;
            }
            if(!fileJustCreated)
            RefreshListUsers();
            
                if (fileJustCreated||StorageUsers.Instance.FindUser(login) == null)
                if (password == repeatPassword)
                {
                    if(email!=null)
                        foreach (User u in _users)
                        {
                            if (u.email == email) return false;
                        }
                    if (login.Length < 3 || login.Length > 15)
                        return false;
                    if (password.Length < 8 || password.Length > 16)
                        return false;
                        if (StorageUsers.Instance.SaveNewUserInFile(new User(login, password, username, email)))
                        return true;
                }

            return false;
        }
        public bool SaveNewUserInFile(User user) 
        {
        if (user == null) return false;

            if (_users.Any(u => u.login == user.login))
            {
                return false;
            }

            _users.Add(user);
            string updatedUsersJ = JsonSerializer.Serialize(_users);
            File.WriteAllText(usersPath, updatedUsersJ);

            RefreshListUsers();
            firstRefreshListUsers=true;
            return true;
        }
        public bool LogIn(string login, string password)
        {
            if (File.Exists(usersPath))
            {
                if (UserVerification(login, password))
                {
                    CurrentUser.Instance.SetCurrentUser(FindUser(login));
                    return true;
                }

            }
            return false;
        }
        public bool UserVerification(string log, string passw )
        {
            if (!firstRefreshListUsers)
            {
                RefreshListUsers();
                firstRefreshListUsers = true;
            }
            if(_users?.FirstOrDefault(u => u.login == log && u.password == passw)!=null)
            return true;
            else return false;
        }


        public User FindUser(string login)
        {
            if (!firstRefreshListUsers)
            {
                RefreshListUsers();
                firstRefreshListUsers = true;
            }
            foreach (var user in _users)
            {
                if (user.login == login)
                {
                    return user;
                }
            }
            return null;
        }

        public bool ChangeCurrentUser(string newUsername,string oldPassword, string newPassword, string newEmail)//финальный метод для смены и сохранения характеристик (типо имени или newEmail) пользователя
        {
            if ((newUsername.Length != 0 && newUsername.Length <= 40)  &&  (newEmail.Contains('@') && newEmail.Length > 3 && newEmail.Length <= 90)  &&  (oldPassword == CurrentUser.Instance.currentUser.password))
            {
                CurrentUser.Instance.currentUser.ChangeUserName(newUsername);
                CurrentUser.Instance.currentUser.ChangeEmail(newEmail);
                CurrentUser.Instance.currentUser.ChangePassword(newPassword);
                UpdateCurrentUser();
                return true;
            }
            return false;       
        }
        public void UpdateCurrentUser()//перезаписывает/обновляет current пользователя в файле
        {
            if (!firstRefreshListUsers)
            {
                RefreshListUsers();
                firstRefreshListUsers = true;
            }
            _users.RemoveAll(cu => cu.login == CurrentUser.Instance.currentUser.login);
            _users.Add(CurrentUser.Instance.currentUser);
            string updatedUsersJ = JsonSerializer.Serialize(_users);//_users
            File.WriteAllText(usersPath, updatedUsersJ);
        }

        public bool AddProjectToUser(string log, RoleEnum role) 
        {
            if (!firstRefreshListUsers)
            {
                RefreshListUsers();
                firstRefreshListUsers = true;
            }
            User found = _users.FirstOrDefault(u => u.login == log);
            if (found == null)
                return false;
            found.AddProjectUsersProjectNamesList();
            _users.RemoveAll(u=>u.login==log);
            _users.Add(found);
            RefreshJsonUsers();
            CurrentProject.Instance.currentProject.AddUserToProject(log, role);
            return true;
        }

        public List<string> GetExistingUserProjects()// готовый метод для возвращения проектов пользователя с проверками на их существование (с удалением не прошедших проверку проектов)
        {
            List<User> updatedUsers = _users;
            updatedUsers.Remove(CurrentUser.Instance.currentUser);
            List<string> Name = new List<string>();
            for(int i=0; i < CurrentUser.Instance.currentUser.GetProjectNames().Count; i++)
            {
                if (Directory.Exists(Path.Combine(AllProjectsPath, CurrentUser.Instance.currentUser.GetProjectNames()[i])))
                {
                    Name.Add(CurrentUser.Instance.currentUser.GetProjectNames()[i]);
                }
                else CurrentUser.Instance.currentUser.RemoveProjectfromProjectNamesList_ByName(CurrentUser.Instance.currentUser.GetProjectNames()[i]);
            }
            updatedUsers.Add(CurrentUser.Instance.currentUser);
            File.WriteAllText(usersPath, JsonSerializer.Serialize(updatedUsers));
            return Name;
        }

        public List<string> GetCurrentUsersProjects()// готовый метод возвращающий лист проектов, где currentuser лидер
        {
            List <string> leadproj = new List<string>();
            List<string> projects = CurrentUser.Instance.currentUser.GetProjectNames();
            foreach (string name in projects)
            {
                string json = File.ReadAllText((Path.Combine(AllProjectsPath, name)));
                List<ProjectUser> team = JsonSerializer.Deserialize<List<ProjectUser>>(json);
                foreach (ProjectUser user in team)
                {
                    if (user.role == RoleEnum.Leader)
                        leadproj.Add(user.userLogin);
                }
            }
            return leadproj;
        }

        public bool RemoveCurrentProjectFromUsersProjectList(string login)
        {
           foreach(User u in _users)
            {
                if (u.login == login)
                {
                    u.RemoveProjectfromProjectNamesList_ByName(login);
                    RefreshJsonUsers();
                    return true;
                }
            }
            return false;
           
        }
    }
}
