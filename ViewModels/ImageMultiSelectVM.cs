using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using WpfPicDB.BusinessLayer;
using WpfPicDB.Models;

namespace WpfPicDB.ViewModels
{
    public class ImageMultiSelectVM : INotifyPropertyChanged
    {
        private readonly PictureBL _pictureBL;
        private ObservableCollection<PictureModel> _pictures;

        public ImageMultiSelectVM()
        {
            
        }

        public ImageMultiSelectVM(IEnumerable<PictureModel> pictures)
        {

        }

        public List<PictureModel> _list = new List<PictureModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void Update()
        {
            _pictures = new ObservableCollection<PictureModel>();
            foreach (PictureModel pic in _pictureBL.Pictures)
            {
                _pictures.Add(pic);
            }
            OnPropertyChanged("Pictures");
        }

        public List<PictureModel> List
        {
            get => this._list;
            set
            {
                if (value != null)
                {
                    this._list = value;
                    OnPropertyChanged(nameof(this.List));
                }
            }
        }



    }
}
