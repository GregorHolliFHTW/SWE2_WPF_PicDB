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
using WpfPicDB.ViewModels;

namespace WpfPicDB.Views
{
    /// <summary>
    /// Interaction logic for ImageTab.xaml
    /// </summary>
    public partial class ImageTab : UserControl
    {
        public ImageTab()
        {
            InitializeComponent();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            ((ImageTabVM)DataContext).UpdateCurrentPicture();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ((ImageTabVM)DataContext).DeleteCurrentPicture();
        }
    }
}
