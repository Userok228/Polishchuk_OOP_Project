﻿using System;
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
using JoraClassLibrary.User.User;
using JoraClassLibrary.Enums;
using JoraClassLibrary.ProjectComponents;

namespace Jora
{
    /// <summary>
    /// Interaction logic for CreationProjectWindow.xaml
    /// </summary>
    public partial class CreationProjectWindow : Window
    {
        public CreationProjectWindow()
        {
            InitializeComponent();
        }

        private void btn_Done_Click(object sender, RoutedEventArgs e)
        {
            if (txtbx_Name.Text.Length > 40|| txtbx_Name.Text.Length<=0)
            {
                txtblk_FormatErrors.Text = "The name length must be greater than 0 and less than 40 characters.";
                return;
            }
            if (txtbx_Description.Text.Length > 2500)
            {
                txtblk_FormatErrors.Text = "The description length must be less than 2500 characters.";
                return;
            }
                DateTime dead;
            if (txtbx_Deadline.Text.Length!=0)
            {
                if (DateTime.TryParseExact(txtbx_Deadline.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dead))
                {
                    if (dead < DateTime.Today)
                    {
                        txtblk_FormatErrors.Text = "The deadline date was entered incorrectly (True format: dd.MM.yyyy). Not earlier than today's date, and not later than 31.12.9999";
                        return;
                    }
                }
                else
                {
                    txtblk_FormatErrors.Text = "The deadline date was entered incorrectly (True format: dd.MM.yyyy). Not earlier than today's date, and not later than 31.12.9999";
                    return;
                }
                if (!StorageProjects.Instance.CreateNewProject(txtbx_Name.Text, txtbx_Description.Text, dead))
                {
                    txtblk_FormatErrors.Text = "Thomething went erong :/";
                    return;
                }
            }
            else
              if (!StorageProjects.Instance.CreateNewProject(txtbx_Name.Text, txtbx_Description.Text, null))
              {   
                  txtblk_FormatErrors.Text = "Thomething went erong :/";
                  return;
              }
            Close();
        }
    }
}

