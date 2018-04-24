using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Models
{
    class HealthStruck :   RealmObject
    {
        public string name { get; set; }
        public string adress { get; set; }
        public string phNumber { get; set; }
        public string email { get; set; }
        public string id { get; set; }
    }
}
