﻿using System;
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
using System.Text.Json;
using JoraClassLibrary.User.User;
using JoraClassLibrary.Enums;
using JoraClassLibrary.ProjectComponents;

namespace Jora
{
    /// <summary>
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private void btn_SignIn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignInPage());
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            if (StorageUsers.Instance.LogIn(txtbx_Login.Text, psswrdbx_Password.Password))
            {
                NavigationService.Navigate(new ProjectSelectionPage_AllProjects());
            }
            else MessageBox.Show("Login or password was entered incorrectly", "The specified user was not found", MessageBoxButton.OK);
        }
    }
}
