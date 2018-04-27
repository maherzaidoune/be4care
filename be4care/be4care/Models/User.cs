using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using be4care.Services;
using Xamarin.Forms;
using be4care.Persistence;

namespace be4care.Models
{
    class User 
    {
        public string name { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phNumber { get; set; }
        public DateTime bDate { get; set; }
        public bool sex { get; set; }
        public string pUrl { get; set; }
        public String username { get; set; }
        
        
    }
}
