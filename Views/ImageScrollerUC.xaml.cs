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
    /// Interaction logic for ImageScrollerUC.xaml
    /// </summary>
    public partial class ImageScrollerUC : UserControl
    {
        public ImageScrollerUC()
        {
            InitializeComponent();
        }

        private void SetPicture_Click(object sender, RoutedEventArgs e)
        {
            ((ImageScrollerVM)DataContext).SetPicture((int)((Button)sender).Tag);
        }
    }
}
