using JoraClassLibrary.User;
using JoraClassLibrary.Enums;
using JoraClassLibrary.ProjectComponents;
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
using System.Windows.Shapes;

namespace Jora
{
    /// <summary>
    /// Interaction logic for TaskInfoWindow.xaml
    /// </summary>
    public partial class TaskInfoWindow : Window
    {
        private ProjectTask task = new ProjectTask();
        private ProjectTask oldtask = new ProjectTask();
        private PriorityTaskEnum currentpriority;
        private PriorityTaskEnum firstpriority;
        private string oldname;
        public TaskInfoWindow(ProjectTask initialTask)
        {
            InitializeComponent();
            oldtask = initialTask;
            this.task = initialTask;
            txtbx_Name.Text = initialTask.Name;
            txtbx_Description.Text = initialTask.Description;
            txtbx_Deadline.Text = initialTask.Deadline?.ToString("dd/MM/yyyy"); ;
            txtbx_CreationDate.Text = initialTask.CreationDate.ToString("dd/MM/yyyy");
            firstpriority = initialTask.Priority;
            currentpriority = initialTask.Priority;
            oldname = initialTask.Name;
            switch (initialTask.Priority)
            {
                case PriorityTaskEnum.Low:
                    btn_Priority1.BorderThickness = new Thickness(4,4,4,4);
                    break;
                case PriorityTaskEnum.Medium:
                    btn_Priority2.BorderThickness = new Thickness(4, 4, 4, 4);
                    break;
                case PriorityTaskEnum.High:
                    btn_Priority3.BorderThickness = new Thickness(4, 4, 4, 4);
                    break;
            }

            if (StorageProjects.Instance.GetCurrentRole() == RoleEnum.Leader)
            {
                txtbx_Name.IsReadOnly = false;
                txtbx_Description.IsReadOnly = false;
                txtbx_Deadline.IsReadOnly = false;
                btn_Priority1.IsHitTestVisible = true;
                btn_Priority2.IsHitTestVisible = true;
                btn_Priority3.IsHitTestVisible = true;
                btn_Ok.Width = 262;
                btn_DeleteTask.Visibility = Visibility.Visible;
            }
        }
        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            task.Name = txtbx_Name.Text;
            if(StorageProjects.Instance.GetCurrentRole()!=RoleEnum.Leader)
                Close();
            if(task.Name != txtbx_Name.Text)
            {
                if(txtbx_Name.Text.Length>40||txtbx_Name.Text.Length==0)
                MessageBox.Show("the length of the task name should not exceed 40 characters");
                return;
            }
            task.Description = txtbx_Description.Text;
            if (task.Description != txtbx_Description.Text&& txtbx_Deadline.Text.Length > 2000)
            {
                MessageBox.Show("the length of the task description should not exceed 2000 characters");
                return;
            }
            if (txtbx_Deadline.Text.Length == 0)
            {
                task.Priority = currentpriority;
                StorageProjects.Instance.ChangeTaskWithSave(CurrentProject.Instance.currentProject.GetColumnNameByTask(oldname), oldname, txtbx_Name.Text, txtbx_Description.Text, currentpriority, null);

            }

            else
            {
                DateTime dead;
                if (DateTime.TryParseExact(txtbx_Deadline.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dead)&&dead>DateTime.Today)
                    task.Deadline = dead;
                else
                {
                    MessageBox.Show("The deadline date was entered incorrectly (True format: dd.MM.yyyy). Not earlier than today's date, and not later than 31.12.9999");
                    return;
                }
                task.Priority = currentpriority;
                StorageProjects.Instance.ChangeTaskWithSave(CurrentProject.Instance.currentProject.GetColumnNameByTask(oldname), oldname, txtbx_Name.Text, txtbx_Description.Text, currentpriority, dead);
            }
            
            this.Close();
        }

        private void btn_Priority2_Click(object sender, RoutedEventArgs e)
        {
            if (StorageProjects.Instance.GetCurrentRole() != RoleEnum.Leader)
                return;
            btn_Priority1.BorderThickness = new Thickness(1, 1, 1, 1);
            btn_Priority2.BorderThickness = new Thickness(4, 4, 4, 4);
            btn_Priority3.BorderThickness = new Thickness(1, 1, 1, 1);
            currentpriority = PriorityTaskEnum.Medium;
        }

        private void btn_Priority1_Click(object sender, RoutedEventArgs e)
        {
            if (StorageProjects.Instance.GetCurrentRole() != RoleEnum.Leader)
                return;
            btn_Priority1.BorderThickness = new Thickness(4, 4, 4, 4);
            btn_Priority2.BorderThickness = new Thickness(1, 1, 1, 1);
            btn_Priority3.BorderThickness = new Thickness(1, 1, 1, 1);
            currentpriority = PriorityTaskEnum.Low;
        }

        private void btn_Priority3_Click(object sender, RoutedEventArgs e)
        {
            if (StorageProjects.Instance.GetCurrentRole() != RoleEnum.Leader)
                return;
            btn_Priority1.BorderThickness = new Thickness(1, 1, 1, 1);
            btn_Priority2.BorderThickness = new Thickness(1, 1, 1, 1);
            btn_Priority3.BorderThickness = new Thickness(4, 4, 4, 4);
            currentpriority = PriorityTaskEnum.High;
        }

        private void btn_DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (StorageProjects.Instance.GetCurrentRole() != RoleEnum.Leader)
                return;
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this issue?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                StorageProjects.Instance.DeleteTask(oldname, CurrentProject.Instance.currentProject.GetColumnNameByTask(oldname));
                Close();
            }
        }
    }
}
