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
    /// Interaction logic for ProjectChatPage.xaml
    /// </summary>
    public partial class ProjectChatPage : Page
    {
        public ProjectChatPage()
        {
            InitializeComponent();
        }

        private void btn_Summary_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectSummaryPage());
        }

        private void btn_Board_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectBoardPage());
        }

        private void btn_Team_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectTeamPage());
        }
    }
}
