using System;
using System.Collections.Generic;
using System.Text;
using WpfPicDB.DataAccessLayer;
using WpfPicDB.Models;
using WpfPicDB.ViewModels;

namespace WpfPicDB.BusinessLayer
{
    public class PhotographerBL
    {
        private readonly DatabaseDAL _databaseDAL;
        private List<PhotographerModel> _photographers;
        private ListPhotographerVM _listPhotographerVM;
        private ImageTabVM _imageTabVM;

        public PhotographerBL(DatabaseDAL databaseDAL)
        {
            _databaseDAL = databaseDAL;
        }

        public List<PhotographerModel> Photographers { get => _photographers; set => _photographers = value; }
        public ListPhotographerVM ListPhotographerVM { get => _listPhotographerVM; set => _listPhotographerVM = value; }
        public ImageTabVM ImageTabVM { get => _imageTabVM; set => _imageTabVM = value; }

        public void AddPhotographer(PhotographerModel photographer)
        {
            _databaseDAL.AddPhotographer(photographer);
            LoadDatabase();
        }

        public void UpdatePhotographer(PhotographerModel photographer)
        {
            _databaseDAL.UpdatePhotographer(photographer);
            LoadDatabase();
        }

        public void DeletePhotographer(PhotographerModel photographer)
        {
            if (photographer.Id != null)
            {
                _databaseDAL.DeletePhotographer((int)photographer.Id);
                LoadDatabase();
            }
        }

        public void LoadDatabase()
        {
            _photographers = _databaseDAL.ReadPhotographers();
            _listPhotographerVM.Update();
            _imageTabVM.UpdatePhotographers();
        }
    }
}
