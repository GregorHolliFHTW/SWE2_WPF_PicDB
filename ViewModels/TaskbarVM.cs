using System;
using System.Collections.Generic;
using System.Text;
using WpfPicDB.BusinessLayer;

namespace WpfPicDB.ViewModels
{
    public class TaskbarVM
    {
        private readonly ListPhotographerVM _listPhotographerVM;
        private readonly EditPhotographerVM _editPhotographerVM;
        private readonly ExportVM _exportVM;
        private readonly PictureBL _pictureBL;
        private readonly PhotographerBL _photographerBL;
        private string _searchstring;

        public TaskbarVM(ListPhotographerVM listPhotographerVM, EditPhotographerVM addPhotographerVM, PictureBL pictureBL, PhotographerBL photographerBL, ExportVM exportVM)
        {
            _listPhotographerVM = listPhotographerVM;
            _editPhotographerVM = addPhotographerVM;
            _exportVM = exportVM;
            _pictureBL = pictureBL;
            _photographerBL = photographerBL;
        }

        public ListPhotographerVM ListPhotographersVM => _listPhotographerVM;
        public EditPhotographerVM EditPhotographerVM => _editPhotographerVM;
        public PictureBL PictureBL => _pictureBL;
        public PhotographerBL PhotographerBL => _photographerBL;

        public string SearchString { get => _searchstring; set => _searchstring = value; }

        public ExportVM ExportVM => _exportVM;

        public void LoadFolder(string path)
        {
            _pictureBL.LoadFolder(path);
        }

        public void PicturesLoadDatabase()
        {
            _pictureBL.LoadDatabase();
        }

        public void PhotographersLoadDatabase()
        {
            _photographerBL.LoadDatabase();
        }

        public void AddCurrentToDatabase()
        {
            _pictureBL.AddCurrentToDatabase();
        }

        public void Search()
        {
            _pictureBL.SearchPictures(_searchstring);
        }

        public void ReportPicture()
        {
            if (_pictureBL.CurrentPicture != null)
            {
                ReportPDF.ReportPicture(_pictureBL.CurrentPicture);
            }
        }

        public void ReportTags()
        {
            if (_pictureBL.Pictures != null)
            {
                ReportPDF.ReportTags(_pictureBL.Pictures);
            }
        }


    }
}
