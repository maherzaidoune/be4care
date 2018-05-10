using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Models
{
    class DocumentsGroupe : List<Document>
    {
        public string  title { get; set; }
        public DocumentsGroupe(string title)
        {
            this.title = title;
        }
    }
}
