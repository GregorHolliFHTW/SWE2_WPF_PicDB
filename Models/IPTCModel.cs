using System;
using System.Collections.Generic;
using System.Text;

namespace WpfPicDB.Models
{
    public class IPTCModel
    {
        private string _title;
        private string _description;
        private string _keywords;

        public string Title { get => _title; set => _title = value; }
        public string Description { get => _description; set => _description = value; }
        public string Keywords { get => _keywords; set => _keywords = value; }

        public IPTCModel(string title = null, string description = null, string keywords = null)
        {
            _title = string.IsNullOrEmpty(title) ? "NA" : title;
            _description = string.IsNullOrEmpty(title) ? "NA" : description;
            _keywords = string.IsNullOrEmpty(title) ? "NA" : keywords;
        }

        public IPTCModel()
        {
            _title = "NA";
            _description = "NA";
            _keywords = "NA";
        }



    }
}
