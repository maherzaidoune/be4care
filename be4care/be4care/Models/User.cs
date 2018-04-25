using Akavache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using be4care.Services;

namespace be4care.Models
{
    class User 
    {
        public String  name { get; set; }
        public String lastName { get; set; }
        public String  email { get; set; }
        public string phNumber { get; set; }
        public DateTime bDate { get; set; }
        public bool sex { get; set; }
        public String pUrl { get; set; }
        public String username { get; set; }

        public async Task<bool> Save()
        {
            try
            {
                await BlobCache.Secure.InsertObject("name", this.name);
                await BlobCache.Secure.InsertObject("lastName", this.lastName);
                await BlobCache.Secure.InsertObject("email", this.email);
                await BlobCache.Secure.InsertObject("phNumber", this.phNumber);
                await BlobCache.Secure.InsertObject("bDate", this.bDate);
                await BlobCache.Secure.InsertObject("sex", this.sex);
                await BlobCache.Secure.InsertObject("pUrl", this.pUrl);
                await BlobCache.Secure.InsertObject("username", this.username);
                return true;
            }
            catch
            {
                return false;
            }
        }







        public  User getUser()
        {
            User user = new User();
              BlobCache.Secure.GetObject<string>("name")
    .Subscribe(x => user.name = x, ex => Console.WriteLine("No name!"));
             BlobCache.Secure.GetObject<string>("lastName")
     .Subscribe(x => user.lastName = x, ex => Console.WriteLine("No lastname!"));
             BlobCache.Secure.GetObject<string>("email")
     .Subscribe(x => user.email = x, ex => Console.WriteLine("No email!"));
             BlobCache.Secure.GetObject<string>("phNumber")
    .Subscribe(x => user.phNumber = x, ex => Console.WriteLine("No phone number!"));
             BlobCache.Secure.GetObject<DateTime>("bDate")
     .Subscribe(x => user.bDate = x, ex => Console.WriteLine("No birth date!"));
             BlobCache.Secure.GetObject<bool>("sex")
     .Subscribe(x => user.sex = x, ex => Console.WriteLine("No  sex!"));
             BlobCache.Secure.GetObject<string>("pUrl")
     .Subscribe(x => user.pUrl = x, ex => Console.WriteLine("No photo url!"));
            BlobCache.Secure.GetObject<string>("username")
     .Subscribe(x => user.username = x, ex => Console.WriteLine("No username!"));
            return user;
        }

        /*
        public Task<bool> UpdateUser()
        {

        }
        */
        
    }
}
