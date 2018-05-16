using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Models
{
    class Document : Favorite
    {
        public int heigh { get {
                return 95;
            } }
        private string _url;
        private string _id;
        private DateTime _date;
        private string _doctor;
        private string _type;
        private string _hStructure;
        private string _place;
        private string _note;
        private string _text;
        public bool isDocument { get {
                return true;
            } }

        public String url { get {
                if (string.IsNullOrEmpty(_url))
                    return "non reconnu";
                return _url;
            } set => _url = value; }
        public string id { get {
                if (string.IsNullOrEmpty(_id))
                    return "non reconnu";
                return _id;
            } set => _id = value; }
        private bool _star;
        private bool _unstar;
        public bool star
        {
            get
            {
                return _star;
            }
            set
            {
                _star = value;
                if (_unstar == value)
                    _unstar = !value;
            }
        }
        public bool unstar
        {
            get
            {
                return !star;
            }
            set
            {
                _unstar = value;
                if (_star == value)
                    _star = !value;
            }
        }

        public DateTime date { get => _date; set => _date = value; }
        public string doctor { get {
                if (string.IsNullOrEmpty(_doctor))
                    return "non reconnu";
                return _doctor;
            } set => _doctor = value; }
        public String dr
        {
            get
            {
                if (!doctor.Contains("Dr"))
                    return "Dr " + doctor;
                return doctor;
            }
            set
            {
                doctor = value;
            }
        }
        public String type { get {
                if (string.IsNullOrEmpty(_type))
                    return "non reconnu";
                return _type;
            } set => _type = value; }
        public String HStructure { get {
                if (string.IsNullOrEmpty(_hStructure))
                    return "non reconnu";
                return _hStructure;
            } set => _hStructure = value; }

        public String place { get {
                if (string.IsNullOrEmpty(_place))
                    return "non  reconnu";
                return _place;
            } set => _place = value; }
        public string note { get {
                if (string.IsNullOrEmpty(_note))
                    return "aucune note";
                return _note;
            } set => _note = value; }
        public string text { get {
                if (string.IsNullOrEmpty(_text))
                    return "text vide";
                return _text;
            } set => _text = value; }
    }
}
