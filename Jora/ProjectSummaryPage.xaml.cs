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
    /// Interaction logic for ProjectSummaryPage.xaml
    /// </summary>
    public partial class ProjectSummaryPage : Page
    {
        public ProjectSummaryPage()
        {
            InitializeComponent();
        }

        private void btn_SaveAdver_Click(object sender, RoutedEventArgs e)
        {
            //Прописать сохранения объявлений в json 
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
    }
}
