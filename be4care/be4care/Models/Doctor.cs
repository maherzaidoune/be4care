using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Models
{
    class Doctor
    {
        public string fullName { get; set; }
        public string adress { get; set; }
        public int phNumber { get; set; } // change this in server !!!!
        public string email { get; set; }
        public string healthStruct { get; set; }
        public string id { get; set; }
    }
}
