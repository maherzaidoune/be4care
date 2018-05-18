using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using be4care.Models;
using Xamarin.Forms;
using System.Linq;
using Akavache;
using System.Reactive.Linq;

namespace be4care.Services
{
    public class UserServices : IUserServices
    {
        public  bool DeleteUser()
        {
            try
            {
                 BlobCache.UserAccount.Invalidate("user");
                 Console.WriteLine("Userservices : user deleted");
            }catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
            return true;
             
        }

        public async  Task<User> GetUser()
        {
            try
            {
                Console.WriteLine("Akavache: "+ BlobCache.LocalMachine.GetType());
                var user = await BlobCache.UserAccount.GetObject<User>("user");
                return user;
            }
            catch(Exception e)
            {
                Console.WriteLine("eeror getting user   "+ e.StackTrace );
                return null;
            }
        }

        public async void SaveUser(User user)
        {
            try
            {
                
                Console.WriteLine("Akavache: " + BlobCache.LocalMachine.GetType());
                DeleteUser();
                await BlobCache.UserAccount.InsertObject("user", user);
                Console.WriteLine("UserServices : User Saved");
            }catch(Exception e)
            {
                Console.WriteLine("error saving user "+e.StackTrace);
            }
            

        }
    }
}
