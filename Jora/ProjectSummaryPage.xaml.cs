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
using System.Text.Json;
using JoraClassLibrary;

namespace Jora
{
    /// <summary>
    /// Interaction logic for ProjectSummaryPage.xaml
    /// </summary>
    public partial class ProjectSummaryPage : Page
    {
        RoleEnum currentRole;
        public ProjectSummaryPage()
        {
            InitializeComponent();
            currentRole = StorageProjects.Instance.GetCurrentRole();
            if (currentRole==RoleEnum.Leader)
            {
                txtbx_Adver.IsEnabled = true;
            }
            txtbx_Adver.Text = StorageProjects.Instance.GetAdver();
            btn_SaveAdver.Visibility = Visibility.Hidden;
            int tasksCount=0;
            foreach (string col in StorageProjects.Instance.GetSummaryInfo())
            {
                if (col != null)
                {
                    scrllvwr_Issues.Items.Add(col);
                    char lastChar = col[col.Length - 1];
                    int.TryParse(lastChar.ToString(), out int res);
                    tasksCount+=res;
                }
            }
            lbl_IssuesTotal.Content = $"You have {tasksCount} issues in total";
        }

        private void btn_SaveAdver_Click(object sender, RoutedEventArgs e)
        {
            if (!StorageProjects.Instance.SaveAdver(txtbx_Adver.Text))
            MessageBox.Show("The advertisements should not be longer than 2500 characters.");
                else
            btn_SaveAdver.Visibility = Visibility.Hidden;
        }

        private void TextBlock_Initialized(object sender, EventArgs e)
        {
            // прописать вывод характеристик с из файла json окна Board
        }

        private void txtbx_Adver_Initialized(object sender, EventArgs e)
        {
            //Подгрузка текста из файла в тектбокс объявления;
            //если роль - лидер то:
            if (txtbx_Adver.Text == string.Empty)
            {
                txtbx_Adver.Text = "Press to change advertisements";
            }
            //также если роль лидер, для текстбокса поставить енейбл
        }


        private void btn_Chat_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectChatPage());
        }

        private void btn_Team_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectTeamPage());
        }

        private void btn_Board_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectBoardPage());
        }

        private void txtbx_Adver_TextChanged(object sender, TextChangedEventArgs e)
        {
            btn_SaveAdver.Visibility = Visibility.Visible;
        }
    }
}
