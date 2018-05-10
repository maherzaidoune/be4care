﻿using System;
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

        private string _id;
        public string id { get {
                if (string.IsNullOrEmpty(_id))
                    return "non reconnu";
                return _id;
            } set {
                _id = value;
            } }


        public string fullName { get {
                if (string.IsNullOrEmpty(fullName))
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
        public bool star { get; set; }
    }
}
