using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using JoraClassLibrary.Enums;
using JoraClassLibrary.ProjectComponents;
using JoraClassLibrary.User;

namespace Jora
{
    /// <summary>
    /// Interaction logic for ProjectBoardPage.xaml
    /// </summary>
    public partial class ProjectBoardPage : Page
    {
        
        public RoleEnum currentrole;
        public static TaskInfoWindow taskwindow;
        public static ChangeColumnWindow changecolumn;
        public static string currentcolumnname;
        public List <Column> LoadingColumns { get; set; }
        public ProjectBoardPage()
        {
            InitializeComponent();
            currentrole = StorageProjects.Instance.GetCurrentRole();
            if (currentrole != RoleEnum.Leader)
            {
                btn_AddColumn.Visibility = Visibility.Hidden;
                rctngl_AddColumn.Visibility = Visibility.Hidden;
            }
            LoadingColumns = StorageProjects.Instance.GetColumns();
            DataContext = this;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void TaskInfo_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var task = button?.Tag as ProjectTask;
            if (task != null && taskwindow == null)
            {
                taskwindow = new TaskInfoWindow(task);
                taskwindow.Closed += (s, args) =>
                {
                    NavigationService.Navigate(new ProjectBoardPage());
                    taskwindow = null;
                };
                taskwindow.Show();
            }
            else taskwindow.Activate();
        }
        private void TaskMouseMove(object sender, MouseEventArgs e)
        {
           if(currentrole!=RoleEnum.Guest)
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var border = sender as Border;
                var task = border?.DataContext as ProjectTask;
                if (task != null)
                {
                    DragDrop.DoDragDrop(border, task, DragDropEffects.Move);
                }
            }
        }

        // Позволяет сброс (визуально и логически)
        private void Drag(object sender, DragEventArgs e)
        {
            if (currentrole != RoleEnum.Guest)
            {

                if (e.Data.GetDataPresent(typeof(ProjectTask)))
                {
                    e.Effects = DragDropEffects.Move;
                }
                else
                {
                    e.Effects = DragDropEffects.None;
                }
                e.Handled = true;
            }
        }

        // Обработка сброса
        private void Drop(object sender, DragEventArgs e)
        {
                if (e.Data.GetDataPresent(typeof(ProjectTask)))
            {
                
                var task = e.Data.GetData(typeof(ProjectTask)) as ProjectTask;
                if (task == null)  return;


                var border = sender as Border;
                var newColumn = border?.Tag as Column;
                if (newColumn == null) return;
               
       
                var oldColumn = LoadingColumns.FirstOrDefault(c => c._tasks.Contains(task));
                StorageProjects.Instance.SaveMovedTask(oldColumn.Name, newColumn.Name, task.Name);
                NavigationService.Navigate(new ProjectBoardPage());

            }
        }
        private void btn_Summary_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectSummaryPage());
        }

        private void btn_Chat_Click(object sender, RoutedEventArgs e)
        {
            if(currentrole!=RoleEnum.Guest)
            NavigationService.Navigate(new ProjectChatPage());
        }

        private void btn_Team_Click(object sender, RoutedEventArgs e)
        {
            if (currentrole != RoleEnum.Guest)
                NavigationService.Navigate(new ProjectTeamPage());
        }

        private void btn_AddColumn_Click(object sender, RoutedEventArgs e)
        {
            scrllvwr_Columns.Visibility=Visibility.Hidden;
            btn_AddColumn.Visibility=Visibility.Hidden;
            rctngl_AddColumn.Visibility=Visibility.Hidden;


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
                StorageProjects.Instance.CreateNewColumn(txtbx_NameAddColumn.Text);
                NavigationService.Navigate(new ProjectBoardPage());

            }
            else lbl_ErrorAddColumn.Visibility = Visibility.Visible;
        }
        private void btn_CancelAddColumn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectBoardPage());
        }

        private void btn_AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (currentrole != RoleEnum.Leader)
            {
                MessageBox.Show("Only the leader can change tasks", "", MessageBoxButton.OK);
                return;
            }
            var button = sender as Button;
            var column = button?.Tag as Column;
            currentcolumnname = column.Name;
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

            scrllvwr_Columns.Visibility = Visibility.Hidden;
            btn_AddColumn.Visibility = Visibility.Hidden;
            rctngl_AddColumn.Visibility = Visibility.Hidden;


        }

        private void btn_DoneAddTask_Click(object sender, RoutedEventArgs e)
        {
            
            if (txtbx_NameAddTask.Text.Length < 1 || txtbx_NameAddTask.Text.Length > 40)
            {
                lbl_ErrorAddTask.Content = "The length of the col name must be greater than 0 and less than 40 characters";
                lbl_ErrorAddTask.Visibility = Visibility.Visible;
                return;
            }
            else
                if (txtbx_DiscriptionAddTask.Text.Length != 0 && txtbx_DiscriptionAddTask.Text.Length > 200)
            {
                lbl_ErrorAddTask.Content = "The length of the col description must be less than 200 characters";
                lbl_ErrorAddTask.Visibility = Visibility.Visible;
                return;
            }
            
           
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
                StorageProjects.Instance.CreateNewTask(currentcolumnname, txtbx_NameAddTask.Text, txtbx_DiscriptionAddTask.Text, dead);
            }
            else
            StorageProjects.Instance.CreateNewTask(currentcolumnname, txtbx_NameAddTask.Text, txtbx_DiscriptionAddTask.Text, null);
            NavigationService.Navigate(new ProjectBoardPage());

            // добавление и сохранение новой задачи
        }

        private void btn_CancelAddTask_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectBoardPage());
        }

        private void btn_ChangeColumn_Click(object sender, RoutedEventArgs e)
        {
            if (currentrole != RoleEnum.Leader)
            {
                MessageBox.Show("Only the leader can change columns", "", MessageBoxButton.OK);
                return;
            }
            var button = sender as Button;
            var col = button?.Tag as Column;
            if (col != null && changecolumn == null)
            {
                changecolumn = new ChangeColumnWindow(col.Name);
                changecolumn.Closed += (s, args) =>
                {
                    NavigationService.Navigate(new ProjectBoardPage());
                    changecolumn = null;
                };
                changecolumn.Show();
            }
            else changecolumn.Activate();
        }
    }
}
