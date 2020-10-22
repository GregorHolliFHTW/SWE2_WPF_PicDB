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
using WpfPicDB.Models;
using WpfPicDB.ViewModels;

namespace WpfPicDB.Views
{
    /// <summary>
    /// Interaction logic for EditPhotographer.xaml
    /// </summary>
    public partial class EditPhotographer : Window
    {
        private readonly PhotographerModel _selected;
        public EditPhotographer(EditPhotographerVM editPhotographerVM, PhotographerModel selected)
        {
            DataContext = editPhotographerVM;
            _selected = selected;
            InitializeComponent();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ((EditPhotographerVM)DataContext).Edit(_selected);
        }
    }
}
