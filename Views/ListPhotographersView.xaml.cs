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
    /// Interaction logic for ListPhotographersView.xaml
    /// </summary>
    public partial class ListPhotographersView : Window
    {
        public ListPhotographersView(ListPhotographerVM listPhotographerVM)
        {
            DataContext = listPhotographerVM;
            InitializeComponent();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            ((ListPhotographerVM)DataContext).Delete((PhotographerModel)PhotographerList.SelectedItem);
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            EditPhotographer window = new EditPhotographer(((ListPhotographerVM)DataContext).EditPhotographerVM, (PhotographerModel)PhotographerList.SelectedItem);
            window.ShowDialog();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            EditPhotographer window = new EditPhotographer(((ListPhotographerVM)DataContext).EditPhotographerVM, (PhotographerModel)PhotographerList.SelectedItem);
            window.ShowDialog();
        }

        private void PhotographerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((ListPhotographerVM)DataContext).ToggleButtons(PhotographerList.SelectedItems.Count);
        }
    }
}
