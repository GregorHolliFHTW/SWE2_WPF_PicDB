using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WpfPicDB.BusinessLayer;
using WpfPicDB.Models;

namespace WpfPicDB.ViewModels
{
    public class ImageTabVM : INotifyPropertyChanged
    {
        private readonly PictureBL _pictureBL;
        private readonly PhotographerBL _photographerBL;
        private IPTCModel _iptc;
        private EXIFModel _exif;
        private string _picphotographer;
        private PhotographerModel _selectedphotographer;
        private List<PhotographerModel> _photographers;

        public event PropertyChangedEventHandler PropertyChanged;

        public ImageTabVM(PictureBL pictureBL, PhotographerBL photographerBL)
        {
            _pictureBL = pictureBL;
            _photographerBL = photographerBL;
            _pictureBL.ImageTabVM = this;
            _photographerBL.ImageTabVM = this;

        }

        public void ReportPicture()
        {
            if (_pictureBL.CurrentPicture != null)
            {
                ReportPDF.ReportPicture(_pictureBL.CurrentPicture);
            }
        }

        public IPTCModel IPTCModel { get => _iptc; set => _iptc = value; }
        public EXIFModel EXIFModel { get => _exif; set => _exif = value; }

        public PhotographerModel SelectedPhotographer { get => _selectedphotographer; set => _selectedphotographer = value; }
        public List<PhotographerModel> Photographers { get => _photographers; set => _photographers = value; }
        public string Picphotographer { get => _picphotographer; set => _picphotographer = value; }

        public PictureBL PictureBL => _pictureBL;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void Update()
        {
            if (_pictureBL.CurrentPicture == null)
            {
                _iptc = null;
                _exif = null;
                _picphotographer = null;
            }
            else
            {
                _iptc = _pictureBL.CurrentPicture.IPTC;
                _exif = _pictureBL.CurrentPicture.EXIF;
                _picphotographer = _pictureBL.CurrentPicture.Photographer.LastName;
            }
            OnPropertyChanged("IPTCModel");
            OnPropertyChanged("EXIFModel");
            OnPropertyChanged("Picphotographer");
        }

        public void UpdatePhotographers()
        {
            _photographers = new List<PhotographerModel>();
            foreach (PhotographerModel pm in _photographerBL.Photographers)
            {
                _photographers.Add(pm);
            }
            OnPropertyChanged("Photographers");
        }

        public void DeleteCurrentPicture()
        {
            _pictureBL.DeleteCurrentPicture();
        }

        public void UpdateCurrentPicture()
        {
            if (_selectedphotographer == null || _selectedphotographer.Id == null)
            {
                _pictureBL.UpdateCurrentPicture((int)_pictureBL.CurrentPicture.Photographer.Id);
                return;
            }
            _pictureBL.UpdateCurrentPicture((int)_selectedphotographer.Id);
        }
    }
}
