using JoraClassLibrary.User.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
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
    /// Interaction logic for ChangeUsersWindow.xaml
    /// </summary>
    public partial class ChangeUsersWindow : Window
    {
        private static bool PasswordIsChanged = false;
        public bool logout = false;
        public ChangeUsersWindow()
        {
            InitializeComponent();
            txtbx_Login.Text = CurrentUser.Instance.currentUser.login;
            txtbx_Email.Text = CurrentUser.Instance.currentUser.email;
            txtbx_UserName.Text = CurrentUser.Instance.currentUser.username;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordIsChanged = true;
            lbl_NewPassword.Visibility = Visibility.Visible;
            lbl_OldPassword.Visibility = Visibility.Visible;
            txtbx_OldPassword.Visibility = Visibility.Visible;
            txtbx_NewPassword.Visibility = Visibility.Visible;

        }

        private void btn_SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            string username = txtbx_UserName.Text;
            string email = txtbx_Email.Text;
            string newpassword = txtbx_NewPassword.Text;

            if (!(username.Length != 0 && username.Length <= 40))
            {
                MessageBox.Show("The username must be larger than 0 and less than 40 characters", "", MessageBoxButton.OK); return;
            }
            if(email.Length>0)
            if (!(email.Contains('@') && email.Length > 3 && email.Length <= 90))
            {
                MessageBox.Show("The email must contain '@'; email must be larger than 3 and less than 90 characters", "", MessageBoxButton.OK); return;
            }
            if(!StorageUsers.Instance.ChangeCurrentUser(username,txtbx_OldPassword.Text, newpassword, email, PasswordIsChanged))
            {
                MessageBox.Show("The old password is not correct or the new password less then 8 characters/larger then 32", "", MessageBoxButton.OK); return;
            }
            else Close();
        }

        private void btn_LogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want log out?", "", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                logout = true;
                Close();
            }
           
        }
    }
}
