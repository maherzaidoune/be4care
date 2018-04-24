using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Models
{
    class User : RealmObject
    {
        public String  name { get; set; }
        public String lastName { get; set; }
        public String  email { get; set; }
        public int phNumber { get; set; }
        public DateTime bDate { get; set; }
        public bool sex { get; set; }
        public String pUrl { get; set; }
        public String username { get; set; }
        public IList<Doctor> myDoctor { get; set; }
        public IList<HealthStruck> MhealthStruck { get; set; }
    }
}
