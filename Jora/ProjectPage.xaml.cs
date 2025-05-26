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
    /// Interaction logic for SummaryPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        public ProjectPage()
        {
            InitializeComponent();
            InProjectFrame.Content = new ProjectSummaryPage();
            lbl_YourRole.Content = "Your role in this project: " + StorageProjects.Instance.GetCurrentRole(); 
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
    }
}
