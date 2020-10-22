using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ookii.Dialogs.Wpf;
using WpfPicDB.ViewModels;

namespace WpfPicDB.Views
{
    /// <summary>
    /// Interaction logic for TaskbarUC.xaml
    /// </summary>
    public partial class TaskbarUC : UserControl
    {
        public TaskbarUC()
        {
            InitializeComponent();
        }

        private void LoadFolder_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                ((TaskbarVM)DataContext).LoadFolder(dialog.SelectedPath);
            }
        }

        private void LoadDatabase_Click(object sender, RoutedEventArgs e)
        {
            ((TaskbarVM)DataContext).PicturesLoadDatabase();
        }

        private void AddToDatabase_Click(object sender, RoutedEventArgs e)
        {
            ((TaskbarVM)DataContext).AddCurrentToDatabase();
        }

        private void AddPhotographer_Click(object sender, RoutedEventArgs e)
        {
            EditPhotographer window = new EditPhotographer(((TaskbarVM)DataContext).EditPhotographerVM, null);
            window.ShowDialog();
        }

        private void ListPhotographer_Click(object sender, RoutedEventArgs e)
        {
            ((TaskbarVM)DataContext).PhotographersLoadDatabase();
            ListPhotographersView window = new ListPhotographersView(((TaskbarVM)DataContext).ListPhotographersVM);
            window.ShowDialog();
        }

        private void ReportPicture_Click(object sender, RoutedEventArgs e)
        {
            ((TaskbarVM)DataContext).ReportPicture();
        }

        private void ReportTags_Click(object sender, RoutedEventArgs e)
        {
            ((TaskbarVM)DataContext).ReportTags();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            ExportView window = new ExportView(((TaskbarVM)DataContext).ExportVM);
            window.ShowDialog();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            ((TaskbarVM)DataContext).Search();
        }
    }
}
