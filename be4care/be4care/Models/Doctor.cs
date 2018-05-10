using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Models
{
    public class Doctor : Contact
    {

        public string id { get; set; }

        private string respectme;
        public string fullName { get {
                if (string.IsNullOrEmpty(respectme))
                    return "non reconnu";
                if (!respectme.Contains("Dr"))
                {
                    return "Dr " + respectme;
                }
                return respectme;
                }
                set {
                    respectme = value;
                } }

        private string _adress;
        public string adress { get {
                if (string.IsNullOrEmpty(_adress))
                    return "non reconnu";
                return _adress;
            } set {
                _adress = value;
            } }

        private string _phNumber;
        public string phNumber { get {
                if (string.IsNullOrEmpty(_phNumber))
                    return "non reconnu";
                return _phNumber;
            } set {
                _phNumber = value;
            } }

        private string _email;
        public string email { get {
                if (string.IsNullOrEmpty(_email))
                    return "non reconnu";
                return _email;
            } set {
                _email = value;
            } }

        private string _healthstruct;
        public string healthStruct { get {
                if (string.IsNullOrEmpty(_healthstruct))
                    return "non  reconnu";
                return _healthstruct;
            } set {
                _healthstruct = value;
            } }

        public bool star { get; set; }

        private string _specialite;
        public string specialite { get {
                if (string.IsNullOrEmpty(_specialite))
                    return "non reconnu";
                return _specialite;
            } set {
                _specialite = value;
            } }

        private string _note;
        public string note { get {
                if (string.IsNullOrEmpty(_note))
                    return "aucune note";
                return _note;
            } set {
                _note = value;
            } }
    }
}
