using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Models
{
    public class HealthStruct  : Contact
    {
        public string fullName { get; set; }
        public string adress { get; set; }
        public string phNumber { get; set; }
        public string email { get; set; }
        public bool star { get; set; }
        public string id { get; set; }
    }
}
