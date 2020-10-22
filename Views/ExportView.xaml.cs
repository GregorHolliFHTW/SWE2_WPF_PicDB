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
using System.Windows.Shapes;
using WpfPicDB.ViewModels;

namespace WpfPicDB.Views
{
    /// <summary>
    /// Interaction logic for ExportView.xaml
    /// </summary>
    public partial class ExportView : Window
    {
        public ExportView(ExportVM exportVM)
        {
            DataContext = exportVM; //or take it out of the parameters and make new one
            InitializeComponent();
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            ((ExportVM)DataContext).ExportPrint();
        }

        private void ZipBtn_Click(object sender, RoutedEventArgs e)
        {
            ((ExportVM)DataContext).ExportZip();
        }


        private void LoadDB_Click(object sender, RoutedEventArgs e)
        {
            ((ExportVM)DataContext).PicturesLoadDatabase();
        }

        private void LoadFolderBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
