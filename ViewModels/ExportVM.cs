using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows;
using WpfPicDB.BusinessLayer;
using WpfPicDB.Models;

namespace WpfPicDB.ViewModels
{
    public class ExportVM : INotifyPropertyChanged
    {
        private readonly PictureBL _pictureBL;
        private ObservableCollection<PictureModel> _pictures;
        //private List<PictureModel> _pictures;
        //private List<ImagePreviewVM> _list = new List<ImagePreviewVM>();
        private string _path = ConfigurationManager.AppSettings["DefaultPath"];

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }


        public ExportVM(PictureBL pictureBL)
        {
            _pictureBL = pictureBL;
            _pictureBL.ExportVM = this;


            /*foreach (var pic in _pictureBL)
            {
                this.List.Add(new ImagePreviewVM())
            }*/

            //foreach bitimage
            //_pictureBL.CurrentPicture.Bitmap;
        }

        public ObservableCollection<PictureModel> Pictures => _pictures; //DataContext for View
        public PictureBL PictureBL => _pictureBL;

        //public IEnumerable<PictureModel> SelectedPics
        //{
        //    get
        //    {
        //        return;
        //    }
        //}

        //public List<ImagePreviewVM> List 
        //{ 
        //    get => _list;
        //    set
        //    {
        //        if (value != null)
        //        {
        //            this._list = value;
        //            OnPropertyChanged(nameof(this.List));
        //        }
        //    }
        //}

        public void Update()
        {
            _pictures = new ObservableCollection<PictureModel>();
            foreach (PictureModel pic in _pictureBL.Pictures)
            {
                _pictures.Add(pic);
            }
            OnPropertyChanged("Pictures");
        }

        public void ExportPrint()
        {
            if (_pictureBL.CurrentPicture != null)
            {                
                ReportPDF.ReportPictureList(_pictures);
            }
        }

        public void ExportZip()
        {
            var saveFile = new SaveFileDialog
            {
                Filter = "Zip Files (*.zip) |*.zip",
                InitialDirectory = ConfigurationManager.AppSettings["DefaultPath"]
            };

            saveFile.ShowDialog();

            var zipFile = saveFile.FileName;
            
            if (File.Exists(zipFile))
            {
                MessageBox.Show("already exists");
            }
            else
            {
                using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
                {
                    foreach (var picture in _pictures)
                    {
                        //archive.CreateEntryFromFile(FilePath, FileName);
                        
                    }
                }
            }
        }

        private void ZipBytes(byte[] imageBytes)
        {
            string filePath = string.Empty;
            using (var ms = new MemoryStream())
            using (var zip = new ZipArchive(ms, ZipArchiveMode.Create))
            {
                var entry = zip.CreateEntry("sample.jpg", CompressionLevel.Optimal);

                using (var entryStream = entry.Open())
                using (var fileToCompressStream = new MemoryStream(imageBytes))
                {
                    fileToCompressStream.CopyTo(entryStream);
                }

                //using (var fs = new FileStream(baseFilePath + "sample.zip", FileMode.Create))
                //{
                //    ms.Position = 0;
                //    ms.WriteTo(fs);
                //}
            }
        }

        public void PicturesLoadDatabase()
        {
            _pictureBL.LoadDatabase();
        }

        public void PicturesLoadFolder()
        {
            _pictureBL.LoadFolder(_path);
        }
    }
}
