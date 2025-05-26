using JoraClassLibrary;
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
using System.Windows.Shapes;

namespace Jora
{
    /// <summary>
    /// Interaction logic for InvitingWindow.xaml
    /// </summary>
    public partial class InvitingWindow : Window
    {
        private static RoleEnum role;
        private static bool rolePressed = false;
        public InvitingWindow()
        {
            InitializeComponent();
        }

        private void btn_Member_Click(object sender, RoutedEventArgs e)
        {
            btn_Member.BorderThickness = new Thickness(4, 4, 4, 4);
            role = RoleEnum.Member;
            rolePressed = true;
        }

        private void btn_Guest_Click(object sender, RoutedEventArgs e)
        {
            btn_Guest.BorderThickness = new Thickness(4, 4, 4, 4);
            role = RoleEnum.Guest;
            rolePressed = true;
        }

        private void btn_Invite_Click(object sender, RoutedEventArgs e)
        {
            if (rolePressed)
            {
                StorageProjects.Instance.AddUserToCurrentProject(txt_login.Text, role);
                StorageUsers.Instance.AddProjectToUser(txt_login.Text);
                Close();
            }
            else MessageBox.Show("Select the role", "User was not added", MessageBoxButton.OK);
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
