using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JoraClassLibrary.User.User;
using JoraClassLibrary.Enums;
using JoraClassLibrary.ProjectComponents;

namespace Jora
{
    /// <summary>
    /// Interaction logic for ProjectTeamPage.xaml
    /// </summary>
    public partial class ProjectTeamPage : Page
    {
        private InvitingWindow invitingwindow;
        private List<ProjectUser> ProjectUsersList { get; set; }
        public ProjectTeamPage()
        {
            InitializeComponent();
            ProjectUsersList = CurrentProject.Instance.currentProject._team;
            ProjectUsers.ItemsSource = ProjectUsersList;
        }

        private void btn_Summary_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectSummaryPage());
        }

        private void btn_Board_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectBoardPage());
        }

        private void btn_Chat_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectChatPage());
        }

        private void btn_UserInfo_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button.DataContext as ProjectUser;
            
            User showeduser = StorageUsers.Instance.FindUser(user.login);

            txtbx_UserName.Text = showeduser.username;
            txtbx_Role.Text = user.role.ToString();
            if (showeduser.email.Length != 0)
                txtbx_Contact.Text = showeduser.email;
            else txtbx_Contact.Text = "The user did not provide contact information";
            txtbx_Login .Text = user.login;
            if(StorageProjects.Instance.GetCurrentRole()==RoleEnum.Leader)
            if (user.role != RoleEnum.Leader)
                btn_RemoveUser.Visibility = Visibility.Visible;
            else btn_RemoveUser.Visibility = Visibility.Hidden;
            lbl_UserName.Visibility = Visibility.Visible;
            lbl_Role.Visibility = Visibility.Visible;
            lbl_Login.Visibility = Visibility.Visible;
            lbl_Contacts.Visibility = Visibility.Visible;
            txtbx_Contact.Visibility = Visibility.Visible;
            txtbx_Login.Visibility = Visibility.Visible;
            txtbx_UserName.Visibility = Visibility.Visible;
            txtbx_Role.Visibility = Visibility.Visible;
        }

        private void btn_Invite_Click(object sender, RoutedEventArgs e)
        {
            if (invitingwindow == null)
            {
                invitingwindow = new InvitingWindow();
                invitingwindow.Closed += (s, args) =>
                {
                    NavigationService.Navigate(new ProjectTeamPage());
                    invitingwindow = null;
                };
                invitingwindow.Show();
            }
            else invitingwindow.Activate();
        }

        private void btn_RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            StorageProjects.Instance.RemoveUserFromProject(txtbx_Login.Text);
            NavigationService.Navigate(new ProjectTeamPage());
        }
    }
}
