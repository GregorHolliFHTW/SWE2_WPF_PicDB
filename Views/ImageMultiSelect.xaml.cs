using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfPicDB.BusinessLayer;
using WpfPicDB.Models;
using WpfPicDB.ViewModels;

namespace WpfPicDB.Views
{
    /// <summary>
    /// Interaction logic for ImageMultiSelect.xaml
    /// </summary>
    public partial class ImageMultiSelect : UserControl
    {

        public ImageMultiSelect() //ImageMultiSelectVM imageMultiSelectVM, PictureBL pictureBL
        {
            //var Pictures = new ObservableCollection<PictureModel>();
            InitializeComponent();
            //DataContext = this;
        }

        //public ObservableCollection<PictureModel> Pictures { get => pictures; set => pictures = value; }
        //public PictureModel SelectedPicture { get => _selectedPicture; set => _selectedPicture = value; }


    }
}
