using System;
using System.Collections.Generic;
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
    /// Interaction logic for ProjectBoardPage.xaml
    /// </summary>
    public partial class ProjectBoardPage : Page
    {
        public ProjectBoardPage()
        {
            InitializeComponent();
        }

        private void btn_Summary_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectSummaryPage());
        }

        private void btn_Chat_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectChatPage());
        }

        private void btn_Team_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectTeamPage());
        }

        private void btn_AddColumn_Click(object sender, RoutedEventArgs e)
        {
            scrllvwr_Tasks.Visibility=Visibility.Hidden;
            btn_AddColumn.Visibility=Visibility.Hidden;
            rctngl_AddColumn.Visibility=Visibility.Hidden;
            btn_AddTask.Visibility=Visibility.Hidden;
            rctngl_AddTask.Visibility=Visibility.Hidden;

            txtbx_NameAddColumn.Clear();
            rctngl_BackGroundAddColumn.Visibility=Visibility.Visible;
            rctngl_DoneAddColumn.Visibility=Visibility.Visible;
            rctngl_CancelAddColumn.Visibility=Visibility.Visible;
            btn_DoneAddColumn.Visibility = Visibility.Visible;
            btn_CancelAddColumn.Visibility = Visibility.Visible;
            txtbx_NameAddColumn.Visibility=Visibility.Visible;
            lbl_NameAddColumn.Visibility=Visibility.Visible;
            lbl_ErrorAddColumn.Visibility=Visibility.Hidden;
        }

        private void btn_DoneAddColumn_Click(object sender, RoutedEventArgs e)
        {
            if (txtbx_NameAddColumn.Text.Length > 0 && txtbx_NameAddColumn.Text.Length < 40)
            {
                rctngl_BackGroundAddColumn.Visibility = Visibility.Hidden;
                rctngl_DoneAddColumn.Visibility = Visibility.Hidden;
                rctngl_CancelAddColumn.Visibility = Visibility.Hidden;
                btn_DoneAddColumn.Visibility = Visibility.Hidden;
                btn_CancelAddColumn.Visibility = Visibility.Hidden;
                txtbx_NameAddColumn.Visibility = Visibility.Hidden;
                lbl_NameAddColumn.Visibility = Visibility.Hidden;
                lbl_ErrorAddColumn.Visibility = Visibility.Hidden;

                scrllvwr_Tasks.Visibility = Visibility.Visible;
                btn_AddColumn.Visibility = Visibility.Visible;
                rctngl_AddColumn.Visibility = Visibility.Visible;
                btn_AddTask.Visibility = Visibility.Visible;
                rctngl_AddTask.Visibility = Visibility.Visible;
                // добавление и сохранение новой колонки
            }
            else lbl_ErrorAddColumn.Visibility = Visibility.Visible;
        }
        private void btn_CancelAddColumn_Click(object sender, RoutedEventArgs e)
        {
            lbl_ErrorAddColumn.Visibility = Visibility.Hidden;
            rctngl_BackGroundAddColumn.Visibility = Visibility.Hidden;
            rctngl_DoneAddColumn.Visibility = Visibility.Hidden;
            rctngl_CancelAddColumn.Visibility = Visibility.Hidden;
            btn_DoneAddColumn.Visibility = Visibility.Hidden;
            btn_CancelAddColumn.Visibility = Visibility.Hidden;
            txtbx_NameAddColumn.Visibility = Visibility.Hidden;
            lbl_NameAddColumn.Visibility = Visibility.Hidden;
            

            scrllvwr_Tasks.Visibility = Visibility.Visible;
            btn_AddColumn.Visibility = Visibility.Visible;
            rctngl_AddColumn.Visibility = Visibility.Visible;
            btn_AddTask.Visibility = Visibility.Visible;
            rctngl_AddTask.Visibility = Visibility.Visible;
        }

        private void btn_AddTask_Click(object sender, RoutedEventArgs e)
        {
            lbl_ErrorAddTask.Visibility = Visibility.Visible;
            rctngl_BackGroundAddTask.Visibility = Visibility.Visible;
            rctngl_DoneAddTask.Visibility = Visibility.Visible;
            rctngl_CancelAddTask.Visibility = Visibility.Visible;
            btn_DoneAddTask.Visibility = Visibility.Visible;
            btn_CancelAddTask.Visibility = Visibility.Visible;
            txtbx_NameAddTask.Visibility = Visibility.Visible;
            lbl_NameAddTask.Visibility = Visibility.Visible;
            lbl_NameAddTask.Visibility = Visibility.Visible;
            lbl_ErrorAddTask.Visibility = Visibility.Visible;
            btn_DoneAddTask.Visibility = Visibility.Visible;
            lbl_DiscriptionAddTask.Visibility = Visibility.Visible;
            lbl_DeadlineAddTask.Visibility = Visibility.Visible;
            txtbx_DiscriptionAddTask.Visibility = Visibility.Visible;
            txtbx_DeadlineAddTask.Visibility = Visibility.Visible;

            lbl_ErrorAddTask.Visibility= Visibility.Hidden;
            txtbx_NameAddTask.Clear();
            txtbx_DiscriptionAddTask.Clear();
            txtbx_DeadlineAddTask.Clear();

            scrllvwr_Tasks.Visibility = Visibility.Hidden;
            btn_AddColumn.Visibility = Visibility.Hidden;
            rctngl_AddColumn.Visibility = Visibility.Hidden;
            btn_AddTask.Visibility = Visibility.Hidden;
            rctngl_AddTask.Visibility = Visibility.Hidden;

        }

        private void btn_DoneAddTask_Click(object sender, RoutedEventArgs e)
        {
            if (txtbx_NameAddTask.Text.Length < 1 || txtbx_NameAddTask.Text.Length > 40)
            {
                lbl_ErrorAddTask.Content = "The length of the task name must be greater than 0 and less than 40 characters";
                lbl_ErrorAddTask.Visibility = Visibility.Visible;
                return;
            }
            else
                if (txtbx_DiscriptionAddTask.Text.Length != 0 && txtbx_DiscriptionAddTask.Text.Length > 200)
            {
                lbl_ErrorAddTask.Content = "The length of the task description must be less than 200 characters";
                lbl_ErrorAddTask.Visibility = Visibility.Visible;
                return;
            }
            else
                 if (txtbx_DeadlineAddTask.Text.Length != 0)
            {
                string deadline = txtbx_DeadlineAddTask.Text;
                DateTime dead;
                if (DateTime.TryParseExact(deadline, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dead))
                {
                    if (dead < DateTime.Today)
                    {
                        lbl_ErrorAddTask.Content = "The deadline date was entered incorrectly (True format: dd.MM.yyyy). Not earlier than today's date, and not later than 31.12.9999";
                        lbl_ErrorAddTask.Visibility = Visibility.Visible;
                        return;
                    }
                }
                else
                {
                    lbl_ErrorAddTask.Content = "The deadline date was entered incorrectly (True format: dd.MM.yyyy). Not earlier than today's date, and not later than 31.12.9999";
                    lbl_ErrorAddTask.Visibility = Visibility.Visible;
                    return;
                }
            }

            lbl_ErrorAddTask.Visibility = Visibility.Hidden;
            rctngl_BackGroundAddTask.Visibility = Visibility.Hidden;
            rctngl_DoneAddTask.Visibility = Visibility.Hidden;
            rctngl_CancelAddTask.Visibility = Visibility.Hidden;
            btn_DoneAddTask.Visibility = Visibility.Hidden;
            btn_CancelAddTask.Visibility = Visibility.Hidden;
            txtbx_NameAddTask.Visibility = Visibility.Hidden;
            lbl_NameAddTask.Visibility = Visibility.Hidden;
            lbl_NameAddTask.Visibility = Visibility.Hidden;
            lbl_ErrorAddTask.Visibility = Visibility.Hidden;
            btn_DoneAddTask.Visibility = Visibility.Hidden;
            lbl_DiscriptionAddTask.Visibility = Visibility.Hidden;
            lbl_DeadlineAddTask.Visibility = Visibility.Hidden;
            txtbx_DiscriptionAddTask.Visibility = Visibility.Hidden;
            txtbx_DeadlineAddTask.Visibility = Visibility.Hidden;

            scrllvwr_Tasks.Visibility = Visibility.Visible;
            btn_AddColumn.Visibility = Visibility.Visible;
            rctngl_AddColumn.Visibility = Visibility.Visible;
            btn_AddTask.Visibility = Visibility.Visible;
            rctngl_AddTask.Visibility = Visibility.Visible;

            // добавление и сохранение новой задачи
        }

        private void btn_CancelAddTask_Click(object sender, RoutedEventArgs e)
        {
            lbl_ErrorAddTask.Visibility = Visibility.Hidden;
            rctngl_BackGroundAddTask.Visibility = Visibility.Hidden;
            rctngl_DoneAddTask.Visibility = Visibility.Hidden;
            rctngl_CancelAddTask.Visibility = Visibility.Hidden;
            btn_DoneAddTask.Visibility = Visibility.Hidden;
            btn_CancelAddTask.Visibility = Visibility.Hidden;
            txtbx_NameAddTask.Visibility = Visibility.Hidden;
            lbl_NameAddTask.Visibility = Visibility.Hidden;
            lbl_NameAddTask.Visibility = Visibility.Hidden;
            lbl_ErrorAddTask.Visibility = Visibility.Hidden;
            btn_DoneAddTask.Visibility = Visibility.Hidden;
            lbl_DiscriptionAddTask.Visibility = Visibility.Hidden;
            lbl_DeadlineAddTask.Visibility = Visibility.Hidden;
            txtbx_DiscriptionAddTask.Visibility = Visibility.Hidden;
            txtbx_DeadlineAddTask.Visibility = Visibility.Hidden;

            scrllvwr_Tasks.Visibility = Visibility.Visible;
            btn_AddColumn.Visibility = Visibility.Visible;
            rctngl_AddColumn.Visibility = Visibility.Visible;
            btn_AddTask.Visibility = Visibility.Visible;
            rctngl_AddTask.Visibility = Visibility.Visible;
        }

        
    }
}
