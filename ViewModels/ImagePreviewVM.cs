using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media.Imaging;
using WpfPicDB.BusinessLayer;

namespace WpfPicDB.ViewModels
{
    public class ImagePreviewVM : INotifyPropertyChanged
    {
        private BitmapImage _imagebitmap;
        private readonly PictureBL _pictureBL;

        public event PropertyChangedEventHandler PropertyChanged;

        public ImagePreviewVM(PictureBL pictureBL)
        {
            _pictureBL = pictureBL;
            _pictureBL.ImagePreviewVM = this;
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void Update()
        {
            if (_pictureBL.CurrentPicture == null)
            {
                _imagebitmap = null;
            }
            else
            {
                _imagebitmap = _pictureBL.CurrentPicture.Bitmap;
            }
            OnPropertyChanged("ImageBitmap");
        }

        public BitmapImage ImageBitmap => _imagebitmap;
    }
}
