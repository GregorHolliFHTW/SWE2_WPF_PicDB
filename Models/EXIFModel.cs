using System;
using System.Collections.Generic;
using System.Text;

namespace WpfPicDB.Models
{
    public class EXIFModel
    {
        private string _cameramodel;
        private string _author;
        private string _datatype;     
        private DateTime _date;

        public string Cameramodel { get => _cameramodel; set => _cameramodel = value; }
        public string Author { get => _author; set => _author = value; }
        public string Datatype { get => _datatype; set => _datatype = value; }
        public DateTime Date { get => _date; set => _date = value; }

        public EXIFModel(DateTime date, string cameramodel = null, string author = null, string datatype = null)
        {
            _date = date;
            _cameramodel = string.IsNullOrEmpty(author) ? "NA" : cameramodel;
            _author = string.IsNullOrEmpty(author) ? "NA" : author;
            _datatype = string.IsNullOrEmpty(author) ? "NA" : datatype;
        }

        public EXIFModel()
        {
            _cameramodel = "NA";
            _author = "NA";
            _datatype = "NA";
            _date = DateTime.Today;
        }
    }
}
