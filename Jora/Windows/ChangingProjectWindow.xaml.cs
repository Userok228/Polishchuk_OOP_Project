using JoraClassLibrary.ProjectComponents;
using JoraClassLibrary.User.User;
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
    /// Interaction logic for ChangingProjectWindow.xaml
    /// </summary>
    public partial class ChangingProjectWindow : Window
    {
        public bool IsCanceled { get; private set; } = false;
        public bool IsDelete { get; private set; } =false;
        private ProjectInfo projectInfo = StorageProjects.Instance.GetProjectInfoByProjectName(CurrentProject.Instance.currentProject.Name);
        public ChangingProjectWindow()
        {
            InitializeComponent();
            txtbx_Name.Text = CurrentProject.Instance.currentProject.Name;
            txtbx_Description.Text = projectInfo.description;
            txtbx_Deadline.Text = projectInfo.deadline?.ToString("dd.MM.yyyy");
            txtbx_CreationDate.Text = projectInfo.creationDate.Date.ToString(("dd.MM.yyyy"));
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            IsCanceled=true;
            Close();
        }


        private void btn_SaveChanges_Click(object sender, RoutedEventArgs e)
        {

            if (txtbx_Deadline.Text.Length == 0)
            {
                projectInfo.deadline = null;
            }
            else
            {
                DateTime dead;
                if (DateTime.TryParseExact(txtbx_Deadline.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dead))
                {
                    projectInfo.deadline = dead;

                    if (dead < DateTime.Today)
                    {
                        MessageBox.Show("The deadline date was entered incorrectly (True format: dd.MM.yyyy). Not earlier than today's date, and not later than 31.12.9999");
                        return;
                    }
                }
                else 
                {
                    MessageBox.Show("The deadline date was entered incorrectly (True format: dd.MM.yyyy). Not earlier than today's date, and not later than 31.12.9999");
                    return;
                }
                projectInfo.deadline = dead;
            }
            StorageProjects.Instance.ChangeCurrentProject_Info(txtbx_Description.Text, projectInfo.deadline);
            if (txtbx_Name.Text!=CurrentProject.Instance.currentProject.Name)
            {
                StorageUsers.Instance.ChangeProjectNameInTeamsProjectNamesLists(CurrentProject.Instance.currentProject._team,CurrentProject.Instance.currentProject.Name, txtbx_Name.Text);
                StorageProjects.Instance.ChangeProjectName(txtbx_Name.Text);
            }
            Close();
           
        }

        private void btn_DeleteProject_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you shure you want delete project?", "", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                IsDelete = true;
                Close();
            }
            
        }
    }
}
