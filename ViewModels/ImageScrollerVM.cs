using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using WpfPicDB.BusinessLayer;
using WpfPicDB.Models;

namespace WpfPicDB.ViewModels
{
    public class ImageScrollerVM : INotifyPropertyChanged
    {
        private readonly PictureBL _pictureBL;
        private ObservableCollection<PictureModel> _pictures;

        public event PropertyChangedEventHandler PropertyChanged;

        public ImageScrollerVM(PictureBL pictureBL)
        {
            _pictureBL = pictureBL;
            _pictureBL.ImageScrollerVM = this;
        }

        public ObservableCollection<PictureModel> Pictures => _pictures;

        public PictureBL PictureBL => _pictureBL;

        public void Update()
        {
            _pictures = new ObservableCollection<PictureModel>();
            foreach (PictureModel pic in _pictureBL.Pictures)
            {
                _pictures.Add(pic);
            }
            OnPropertyChanged("Pictures");
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void SetPicture(int index)
        {
            _pictureBL.SetPicture(index);
        }
    }
}
