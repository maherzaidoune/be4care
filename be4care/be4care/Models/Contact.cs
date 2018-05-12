using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Models
{
    public  class Contact : Favorite
    {
        public string fullName { get; set; }
        public bool star { get; set; }
    }
}
