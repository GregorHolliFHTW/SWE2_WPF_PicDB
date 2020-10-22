using System;
using System.Collections.Generic;
using System.Text;

namespace WpfPicDB.Models
{
    public class PhotographerModel
    {
        private Nullable<int> _id;
        private string _firstname;
        private string _lastname;
        private DateTime _birthdate;

        public PhotographerModel()
        {
            _id = null;
        }

        public PhotographerModel(Nullable<int> id, string firstname, string lastname, DateTime birthdate)
        {
            _id = id;
            _firstname = firstname;
            _lastname = lastname;
            _birthdate = birthdate;
        }



        public Nullable<int> Id { get => _id; set => _id = value; }
        public string FirstName { get => _firstname; set => _firstname = value; }
        public string LastName { get => _lastname; set => _lastname = value; }
        public DateTime Birthdate { get => _birthdate; set => _birthdate = value; }

    }
}
