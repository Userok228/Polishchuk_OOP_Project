using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using JoraClassLibrary.ProjectComponents;
using JoraClassLibrary.Enums;

namespace JoraClassLibrary.User.User
{
    public class StorageUsers
    {
        public string usersPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"), "UsersJ.json");
        private string AllProjectsPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"), "AllProjects");

        private static StorageUsers _SU;
        private static readonly object _lock = new object();
        public List<User> _users = new List<User>();
        private bool firstRefreshListUsers = false;
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
                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora")))
                {
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"));
                }
                using (File.Create(usersPath)) { };
                fileJustCreated = true;
            }
            if (!fileJustCreated)
                RefreshListUsers();

            if (fileJustCreated || Instance.FindUser(login) == null)
                if (password == repeatPassword)
                {
                    if (email != null)
                    {
                        foreach (User u in _users)
                        {
                            if (u.email == email) return false;
                        }
                        if (email.Length == 0 || email.Length > 90) return false;
                    }
                    if (login.Length < 3 || login.Length > 32)
                        return false;
                    if (username.Length<3|| username.Length>40)
                    if (password.Length < 8 || password.Length > 32)
                        return false;
                    if (Instance.SaveNewUserInFile(new User(login, password, username, email)))
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
            firstRefreshListUsers = true;
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
        public bool UserVerification(string log, string passw)
        {
            if (!firstRefreshListUsers)
            {
                RefreshListUsers();
                firstRefreshListUsers = true;
            }
            if (_users?.FirstOrDefault(u => u.login == log && u.password == passw) != null)
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

        public bool ChangeCurrentUser(string newUsername,string oldPassword, string newPassword, string newEmail, bool pass)//финальный метод для смены и сохранения характеристик (типо имени или newEmail) пользователя
        {
            if (pass)
            {
                if (oldPassword == CurrentUser.Instance.currentUser.password)
                {
                    if(newPassword.Length>8&&newPassword.Length<32)
                    CurrentUser.Instance.currentUser.ChangePassword(newPassword);
                    else return false;
                }
                else return false;
            }
            if (newUsername != CurrentUser.Instance.currentUser.username)
                CurrentUser.Instance.currentUser.ChangeUserName(newUsername);
            if(newEmail != CurrentUser.Instance.currentUser.email)
                CurrentUser.Instance.currentUser.ChangeEmail(newEmail);
            
                UpdateCurrentUser();
                return true;

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

        public bool AddProjectToUser(string log, string ProjName)
        {
            if (!firstRefreshListUsers)
            {
                RefreshListUsers();
                firstRefreshListUsers = true;
            }
            User found = _users.FirstOrDefault(u => u.login == log);
            if (found == null)
                return false;
            found.AddProjectUsersProjectNamesList(ProjName);
            _users.RemoveAll(u => u.login == log);
            _users.Add(found);
            RefreshJsonUsers();
            return true;
        }
        public void ChangeProjectNameInTeamsProjectNamesLists(List<ProjectUser> team,string oldProjName, string newProjName)
        {
            for(int i=0; i < team.Count; i++)
            {
                User found = FindUser(team[i].login);
                for(int j=0; j<found.GetProjectNames().Count(); j++)
                {
                    if (found.GetProjectNames()[j] == oldProjName)
                    {
                        found.RemoveProjectfromProjectNamesList(oldProjName);
                        AddProjectToUser(team[i].login, newProjName);
                        return;
                    }
                }
            }
        }

        public List<string> GetExistingUserProjects()// готовый метод для возвращения проектов пользователя с проверками на их существование (с удалением не прошедших проверку проектов)
        {
            List<User> updatedUsers = _users;
            updatedUsers.Remove(CurrentUser.Instance.currentUser);
            List<string> Name = new List<string>();
            for (int i = 0; i < CurrentUser.Instance.currentUser.GetProjectNames().Count; i++)
            {
                if (Directory.Exists(Path.Combine(AllProjectsPath, CurrentUser.Instance.currentUser.GetProjectNames()[i])) && StorageProjects.Instance.UserNotRemovedFromProjectCheck(CurrentUser.Instance.currentUser.GetProjectNames()[i]))
                {
                    Name.Add(CurrentUser.Instance.currentUser.GetProjectNames()[i]);
                }
                else CurrentUser.Instance.currentUser.RemoveProjectfromProjectNamesList(CurrentUser.Instance.currentUser.GetProjectNames()[i]);
            }
            updatedUsers.Add(CurrentUser.Instance.currentUser);
            File.WriteAllText(usersPath, JsonSerializer.Serialize(updatedUsers));
            return Name;
        }

        public List<string> GetCurrentUsersProjects()// готовый метод возвращающий лист проектов, где currentuser лидер
        {
            List<string> leadproj = new List<string>();
            List<string> projects = CurrentUser.Instance.currentUser.GetProjectNames();
            foreach (string name in projects)
            {
                string json = File.ReadAllText(Path.Combine(AllProjectsPath, name));
                List<ProjectUser> team = JsonSerializer.Deserialize<List<ProjectUser>>(json);
                foreach (ProjectUser user in team)
                {
                    if (user.role == RoleEnum.Leader)
                        leadproj.Add(user.login);
                }
            }
            return leadproj;
        }

        public bool RemoveCurrentProjectFromUsersProjectList(string login)
        {
            foreach (User u in _users)
            {
                if (u.login == login)
                {
                    u.RemoveProjectfromProjectNamesList(login);
                    RefreshJsonUsers();
                    return true;
                }
            }
            return false;

        }
    }
}
