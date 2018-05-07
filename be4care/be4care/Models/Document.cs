using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Models
{
    class Document 
    {
        public String url { get; set; }
        public string id { get; set; }
        public bool star { get; set; }
        public DateTime date { get; set; }
        public string doctor { get; set; }
        public String dr { get {
                return "Dr " + doctor;
            } set {
                doctor = value;
            } }
        public String type { get; set; }
        public String HStructure { get; set; }
        public String place { get; set; }
        public string note { get; set; }
        public string text { get; set; }
    }
}
