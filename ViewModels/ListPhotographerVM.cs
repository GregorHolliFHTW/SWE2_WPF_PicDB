using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WpfPicDB.BusinessLayer;
using WpfPicDB.Models;

namespace WpfPicDB.ViewModels
{
    public class ListPhotographerVM : INotifyPropertyChanged
    {
        private readonly EditPhotographerVM _editPhotographerVM;
        private List<PhotographerModel> _photographers;
        private readonly PhotographerBL _photographerBL;
        private bool _isselected;


        public event PropertyChangedEventHandler PropertyChanged;

        public ListPhotographerVM(EditPhotographerVM editPhotographerVM, PhotographerBL photographerBL)
        {
            _editPhotographerVM = editPhotographerVM;
            _photographerBL = photographerBL;
            _isselected = false;
            _photographerBL.ListPhotographerVM = this;
            _photographerBL.LoadDatabase();
        }

        public bool IsSelected => _isselected;
        public EditPhotographerVM EditPhotographerVM => _editPhotographerVM;
        public List<PhotographerModel> Photographers => _photographers;
        public PhotographerBL PhotographerBL => _photographerBL;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void Update()
        {
            _photographers = new List<PhotographerModel>();
            foreach (PhotographerModel phg in _photographerBL.Photographers)
            {
                _photographers.Add(phg);
            }
            OnPropertyChanged("Photographers");
        }

        public void ToggleButtons(int count)
        {
            if (count != 0)
            {
                _isselected = true;
            }
            else
            {
                _isselected = false;
            }
            OnPropertyChanged("IsSelected");
        }

        public void Delete(PhotographerModel photographer)
        {
            _photographerBL.DeletePhotographer(photographer);
        }



    }
}
