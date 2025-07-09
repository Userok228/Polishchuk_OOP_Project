using JoraClassLibrary.Enums;
using JoraClassLibrary.ProjectComponents;
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
using JoraClassLibrary.User;


namespace Jora
{
    /// <summary>
    /// Interaction logic for SummaryPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        private ChangeUsersWindow changeuserswindow;
        private ChangingProjectWindow changingprojectwindow;
        public ProjectPage()
        {
            InitializeComponent();
            InProjectFrame.Content = new ProjectSummaryPage();
            lbl_YourRole.Content = "Your role in this project: " + StorageProjects.Instance.GetCurrentRole();
            if (StorageProjects.Instance.GetCurrentRole() == RoleEnum.Leader)
            {
                btn_ChangeProject.Visibility = Visibility.Visible;
            }
        }
        private void btn_Projects_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectSelectionPage_AllProjects());
        }
        private void btn_CreateNewProject_Click(object sender, RoutedEventArgs e)
        {      
            var page = new ProjectSelectionPage_AllProjects();
            page.CreationProjectFromOtherPage();
        }

        private void btn_ChangeProject_Click(object sender, RoutedEventArgs e)
        {

            if (changingprojectwindow == null)
            {
                changingprojectwindow = new ChangingProjectWindow();
                changingprojectwindow.Closed += (s, args) =>
                {
                    if (changingprojectwindow.IsDelete)
                    {
                        changingprojectwindow = null;
                        StorageProjects.Instance.DeleteProject(CurrentProject.Instance.currentProject.Name);
                        NavigationService.Navigate(new ProjectSelectionPage_AllProjects());
                    }
                    else
                    if (changingprojectwindow.IsCanceled)
                    {
                        changingprojectwindow = null;
                    }
                    else
                    {
                        changingprojectwindow = null;
                        NavigationService.Navigate(new ProjectSelectionPage_AllProjects());
                    }
                    
                };
                changingprojectwindow.Show();
            }
            else changingprojectwindow.Activate();
        }

        private void btn_Profile_Click(object sender, RoutedEventArgs e)
        {

                if (changeuserswindow == null)
                {
                    changeuserswindow = new ChangeUsersWindow();
                    changeuserswindow.Closed += (s, args) =>
                    {
                        
                        if (changeuserswindow.logout)
                        {
                            changeuserswindow = null;
                            NavigationService.Navigate(new LogInPage());
                        }
                        else changeuserswindow = null;

                    };
                    changeuserswindow.Show();
                }
                else changeuserswindow.Activate();
        }
    }
}
