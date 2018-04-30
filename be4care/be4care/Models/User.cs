using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using be4care.Services;
using Xamarin.Forms;

namespace be4care.Models
{
    public class User 
    {
        public int  localId { get; set; }
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
