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
using System.Windows.Shapes;
using JoraClassLibrary.User.User;
using JoraClassLibrary.Enums;
using JoraClassLibrary.ProjectComponents;

namespace Jora
{
    /// <summary>
    /// Interaction logic for ChangeColumnWindow.xaml
    /// </summary>
    public partial class ChangeColumnWindow : Window
    {
        string oldname;
        public ChangeColumnWindow(string oldColumnName)
        {
            InitializeComponent();
            txtbx_Name.Text = oldColumnName;
            oldname = oldColumnName;
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if(StorageProjects.Instance.ChangeColumnNameCurrentProject(oldname, txtbx_Name.Text))
                Close();
            else lbl_Error.Visibility = Visibility.Visible;
            
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this column?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (!StorageProjects.Instance.DeleteColumn(oldname))
                    MessageBox.Show("To delete, the column must be empty", "Error", MessageBoxButton.OK);
                else
                Close();
            }
        }

        private void btn_CreateNewTask_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
