using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Models
{
    class Document 
    {
        public String url { get; set; }
        public bool star { get; set; }
        public DateTime date { get; set; }
        public String dr { get; set; }
        public String type { get; set; }
        public String HStructure { get; set; }
        public String place { get; set; }
        public String id { get; set; }
    }
}
