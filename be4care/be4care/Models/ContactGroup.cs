using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Models
{
    class ContactGroup : List<Contact>
    {
        public string title { get; set; }
        public ContactGroup(string title)
        {
            this.title = title;
        }
    }
}
