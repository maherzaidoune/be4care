using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Models
{
    public class Doctor : Contact
    {
        public string respectme;
        public string fullName { get {
                return "Dr " + respectme;
            } set {
                respectme = value;
            } }
        public string adress { get; set; }
        public string phNumber { get; set; } // change this in server !!!!
        public string email { get; set; }
        public string healthStruct { get; set; }
        public string id { get; set; }
        public bool star { get; set; }
        public string specialite { get; set; }
        public string note { get; set; }
    }
}
