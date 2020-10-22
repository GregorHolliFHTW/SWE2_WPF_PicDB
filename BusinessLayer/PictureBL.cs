using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfPicDB.DataAccessLayer;
using WpfPicDB.Models;
using WpfPicDB.ViewModels;

namespace WpfPicDB.BusinessLayer
{
    public class PictureBL
    {
        private List<PictureModel> _pictures;
        private PictureModel _current;
        private readonly FileDAL _fileDAL;
        private readonly DatabaseDAL _databaseDAL;
        private ImagePreviewVM _imagePreviewVM;
        private ImageScrollerVM _imageScrollerVM;
        private ImageTabVM _imageTabVM;
        private ExportVM _exportVM;
        
        public PictureBL(FileDAL fileDAL, DatabaseDAL databaseDAL)
        {
            _fileDAL = fileDAL;
            _databaseDAL = databaseDAL;
        }

        public List<PictureModel> Pictures => _pictures;
        public PictureModel CurrentPicture => _current;

        public ImagePreviewVM ImagePreviewVM { get => _imagePreviewVM; set => _imagePreviewVM = value; }
        public ImageScrollerVM ImageScrollerVM { get => _imageScrollerVM; set => _imageScrollerVM = value; }
        public ImageTabVM ImageTabVM { get => _imageTabVM; set => _imageTabVM = value; }
        public ExportVM ExportVM { get => _exportVM; set => _exportVM = value; }

        public void LoadFolder(string path)
        {
            _pictures = _fileDAL.LoadFolder(path);
            _current = (_pictures.Count != 0) ? _pictures.First() : null;
            _imagePreviewVM.Update();
            _imageScrollerVM.Update();
            //_exportVM.Update();
        }

        public void LoadDatabase()
        {
            _pictures = _databaseDAL.ReadAllPictures();
            _current = (_pictures.Count != 0) ? _pictures.First() : null;
            _imagePreviewVM.Update();
            _imageScrollerVM.Update();
            _imageTabVM.Update();
            _exportVM.Update();
        }

        public void AddCurrentToDatabase()
        {
            if (_current == null)
            {
                return;
            }
            _databaseDAL.AddPicture(_current, _current.Photographer.Id);
            LoadDatabase();
        }

        public void SearchPictures(string query)
        {
            _pictures = _databaseDAL.SearchPictures(query);
            _current = (_pictures.Count != 0) ? _pictures.First() : null;
            _imagePreviewVM.Update();
            _imageScrollerVM.Update();
            _imageTabVM.Update();
        }

        public void DeleteCurrentPicture()
        {
            if (_current.Id != 0)
            {
                _databaseDAL.DeletePicture(_current.Id);
                LoadDatabase();
            }
        }

        public void UpdateCurrentPicture(int photographerid)
        {
            if (_current == null)
            {
                return;
            }
            _current.Photographer.Id = photographerid;
            AddCurrentToDatabase();
        }

        public void SetPicture(int index)
        {
            _current = _pictures[index];
            _imagePreviewVM.Update();
            _imageTabVM.Update();
        }
        
    }
}
