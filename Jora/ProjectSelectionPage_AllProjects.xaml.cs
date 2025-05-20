using JoraClassLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.IO;

namespace Jora
{
    /// <summary>
    /// Interaction logic for ProjectSelectionPage_AllProjects.xaml
    /// </summary>
    public partial class ProjectSelectionPage_AllProjects : Page
    {
        public static CreationProjectWindow creationwindow;
        private static string selectedProject;

        public ObservableCollection <string> ProjectNames { get; set; }
        public ProjectSelectionPage_AllProjects()
        {    
            InitializeComponent();
            ProjectNames = new ObservableCollection<string>(StorageUsers.Instance.GetExistingUserProjects());
            DataContext = this;
        }
        private void btn_Projects_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

           ProjectInfo pi = StorageProjects.Instance.GetProjectInfoByProjectName(button.Content.ToString());
            lbl_ProjectName.Content = button.Content.ToString();
            if (pi.deadline == null)
                lbl_ProjectDeadline.Content = "Project deadline not specified";
            else
            lbl_ProjectDeadline.Content = pi.deadline?.ToString("dd-MM-yyyy");
            lbl_ProjectCreationDate.Content = pi.creationDate.Date.ToString(("dd-MM-yyyy"));
            if (pi.description.Length == 0)
                txtblk_Description.Text = "Project description not specified";
            else
            txtblk_Description.Text = pi.description;

            lbl_PN.Visibility = Visibility.Visible;
            lbl_PD.Visibility = Visibility.Visible;
            lbl_PDL.Visibility = Visibility.Visible;
            lbl_PCD.Visibility = Visibility.Visible;
            lbl_ProjectName.Visibility = Visibility.Visible;
            scrllvwr_Description.Visibility = Visibility.Visible;
            txtblk_Description.Visibility = Visibility.Visible;
            lbl_ProjectDeadline.Visibility = Visibility.Visible;
            lbl_ProjectCreationDate.Visibility = Visibility.Visible;
            btn_OpenProject.Visibility = Visibility.Visible;
            selectedProject = button.Content.ToString();

           // NavigationService.Navigate(new ProjectPage());
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
        public void CreationProjectFromOtherPage() 
        {
            if (creationwindow == null)
            {
                creationwindow = new CreationProjectWindow();
                creationwindow.Closed += (s, args) =>
                {
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

        private void btn_OpenProject_Click(object sender, RoutedEventArgs e)
        {
            StorageProjects.Instance.OpenProject(selectedProject);
            NavigationService.Navigate(new ProjectPage());
        }
    }
}
