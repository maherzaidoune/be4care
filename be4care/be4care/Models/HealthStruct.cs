using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Models
{
    public class HealthStruct : Contact
    {
        private string _fullName;
        private string _adress;
        private string _phNumber;
        private string _email;

        public string id { get; set; }



        public string fullName { get {
                if (string.IsNullOrEmpty(_fullName))
                    return "non reconnu";
                return _fullName;
            } set => _fullName = value; }
        public string adress { get {
                if (string.IsNullOrEmpty(_adress))
                    return "non reconnu";
                return _adress;
            } set => _adress = value; }
        public string phNumber { get {
                if (string.IsNullOrEmpty(_phNumber))
                    return "non reconnu";
                return _phNumber;
            } set => _phNumber = value; }
        public string email { get {
                if (string.IsNullOrEmpty(_email))
                    return "non reconnu";
                return _email;
            } set => _email = value; }
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

    }
}
