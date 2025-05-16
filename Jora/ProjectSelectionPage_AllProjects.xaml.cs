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
    /// Interaction logic for ProjectSelectionPage_AllProjects.xaml
    /// </summary>
    public partial class ProjectSelectionPage_AllProjects : Page
    {
        public static CreationProjectWindow creationwindow;

        public ProjectSelectionPage_AllProjects()
        {
            InitializeComponent();
        }

        private void btn_NewProject_Click(object sender, RoutedEventArgs e)
        {
            if (creationwindow == null)
            {
                creationwindow = new CreationProjectWindow();
                creationwindow.Show();
            }
            else creationwindow.Activate();
         
        }

        private void btn_Project1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectPage());
        }

        private void btn_CreateNewProject_Click(object sender, RoutedEventArgs e)
        {
            if (creationwindow == null)
            {
                creationwindow = new CreationProjectWindow();
                creationwindow.Show();
            }
            else creationwindow.Activate();
        }
    }
}
