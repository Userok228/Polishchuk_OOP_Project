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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
