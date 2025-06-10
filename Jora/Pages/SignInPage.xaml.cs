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
    /// Interaction logic for SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        private void btn_Done_Click(object sender, RoutedEventArgs e)
        {
           if(txtbx_Login.Text.Length == 0|| txtbx_Login.Text.Length >= 40)
            {

            }
            if (!(txtbx_Username.Text.Length != 0 && txtbx_Username.Text.Length <= 40))
            {
                MessageBox.Show("The username must be larger than 0 and less than 40 characters", "", MessageBoxButton.OK); return;
            }
            if(txtbx_Email.Text.Length!=0)
            if (!(txtbx_Email.Text.Contains('@') && txtbx_Email.Text.Length >= 3 && txtbx_Email.Text.Length <= 90))
            {
                MessageBox.Show("The email must contain '@'; email must be larger than 3 and less than 90 characters", "", MessageBoxButton.OK); return;
            }
            if (psswrdbx_Password.Password.Length<8|| psswrdbx_Password.Password.Length >32)
            {
                MessageBox.Show("The old password is not correct or the new password less then 8 characters/larger then 32", "", MessageBoxButton.OK); return;
            }
            if (StorageUsers.Instance.CreationNewUser(txtbx_Login.Text, psswrdbx_Password.Password, psswrdbx_RepeatPassword.Password, txtbx_Username.Text, txtbx_Email.Text))
            {
                MessageBox.Show("New user successfully created");
                NavigationService.Navigate(new LogInPage());
            }
            else
            {
                MessageBox.Show("A user with this login already exists or the user data was entered incorrectly.", "Error", MessageBoxButton.OK);
            }

        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LogInPage());
        }
    }
}
