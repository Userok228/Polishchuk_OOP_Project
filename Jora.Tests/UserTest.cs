using JoraClassLibrary;
using JoraClassLibrary.Enums;
using JoraClassLibrary.ProjectComponents;
using JoraClassLibrary.User.User;
using System.Xml.Linq;
using System;

namespace Jora.Tests
{
    public class UserTest
    {
        public string UsersPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jora"), "UsersJ.json");
        [Theory]
        [InlineData("", "password", "password", "username", "@email")]
        [InlineData("log", "password", "password111", "username", "@email")]
        [InlineData("log", "pass", "pass", "username", "@email")]
        [InlineData("log", "password", "password", "", "@email")]
        [InlineData("log", "password", "password", "usernameeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee", "e@mail")]
        [InlineData("log", "password", "password", "username", "email")]
        [InlineData("log", "password", "password", "username", "@")]
        public void CreationUserFail(string login, string password, string repeatPassword, string username, string email)
        {
            File.Delete(UsersPath);
            bool succes = StorageUsers.Instance.CreationNewUser(login, password, repeatPassword, username, email);
            Assert.False(succes);
        }
        [Fact]
        public void CreationUserSucces()
        {
            File.Delete(UsersPath);
            Assert.True(StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@mail"));

        }
        [Fact]

        public void LoginUserSucces()
        {
            File.Delete(UsersPath);

            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.LogIn("login", "password");

            Assert.True(CurrentUser.Instance.currentUser != null);
        }
        [Theory]
        [InlineData("log", "password")]
        [InlineData("login", "passwordddd")]
        public void LoginUserFail(string login, string password)
        {
           
            File.Delete(UsersPath);

            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");

            bool succes = StorageUsers.Instance.LogIn(login, password);
            Assert.False(succes);
        }
        [Fact]

        public void ChangeUserSucces()
        {
            File.Delete(UsersPath);

            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.LogIn("login", "password");
            bool succes = StorageUsers.Instance.ChangeCurrentUser("newUsername", "password", "newpassword", "@eee", true);

            Assert.True(succes);
        }
        [Theory]
        [InlineData("username", "password", "password", "@email")]
        [InlineData("", "password", "newpassword", "@eee")]
        [InlineData("username", "password", "password", "@eeee")]
        [InlineData("username", "password", "newpassword", "@e")]
        [InlineData("username", "password", "newpassword", "email")]
        public void ChangeUserFail(string newusername, string password, string newpassword, string newemail)
        {
            File.Delete(UsersPath);
            StorageUsers.Instance.CreationNewUser("login", "password", "password", "username", "@email");
            StorageUsers.Instance.LogIn("login", "password");
            Assert.False(StorageUsers.Instance.ChangeCurrentUser(newusername, password, newpassword, newemail, true));

        }
    }

}
