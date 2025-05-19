using JoraClassLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for ProjectSelectionPage_AllProjects.xaml
    /// </summary>
    public partial class ProjectSelectionPage_AllProjects : Page
    {
        public static CreationProjectWindow creationwindow;

        public ObservableCollection <string> ProjectNames { get; set; }
        public ProjectSelectionPage_AllProjects()
        {    
            InitializeComponent();
            ProjectNames = new ObservableCollection<string>(StorageUsers.Instance.GetExistingUserProjects());
            DataContext = this;
        }
        private void btn_Projects_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            string projectName = button?.Content.ToString();

            NavigationService.Navigate(new ProjectPage());
        }


        private void btn_CreateNewProject_Click(object sender, RoutedEventArgs e)
        {
            if (creationwindow == null)
             {
                 creationwindow = new CreationProjectWindow();
                creationwindow.Closed += (s, args) =>
                {
                    NavigationService.Navigate(new ProjectSelectionPage_AllProjects());
                    creationwindow = null;
                };
                creationwindow.Show();
             }
             else creationwindow.Activate();
        }
        private void btn_YourProjects_Click(object sender, RoutedEventArgs e)
        {
            ProjectNames = new ObservableCollection<string>(StorageUsers.Instance.GetCurrentUsersProjects());
            DataContext = this;
        }
    }
}
