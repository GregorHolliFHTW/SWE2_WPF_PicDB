using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WpfPicDB.BusinessLayer;
using WpfPicDB.Models;

namespace WpfPicDB.ViewModels
{
    public class EditPhotographerVM : IDataErrorInfo
    {
        private readonly PhotographerBL _photographerBL;
        private string _firstname;
        private string _lastname;
        private DateTime _date;

        public string FirstName { get => _firstname; set => _firstname = value; }
        public string LastName { get => _lastname; set => _lastname = value; }
        public DateTime Date { get => _date; set => _date = value; }

        public EditPhotographerVM(PhotographerBL photographerBL)
        {
            _photographerBL = photographerBL;
        }

        //public string this[string property] => throw new NotImplementedException();
        public string this[string propertyname]
        {
            get
            {
                string result = String.Empty;
                switch (propertyname)
                {
                    case "FirstName":
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            result = "First Name cant be empty!";
                        }
                        break;
                    case "LastName":
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            result = "Last Name cant be empty!";
                        }
                        break;
                }
                return result;
            }
        }

        public string Error => throw new NotImplementedException();

        public void Edit(PhotographerModel selected)
        {
            if (selected == null)
            {
                _photographerBL.AddPhotographer(new PhotographerModel(null, _firstname, _lastname, _date));
                return;
            }
            _photographerBL.UpdatePhotographer(new PhotographerModel(selected.Id, _firstname, _lastname, _date));
        }
    }
}
