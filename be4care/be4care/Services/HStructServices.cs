using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using System.Reactive.Linq;
using be4care.Models;

namespace be4care.Services
{
    public class HStructServices : IHStructServices
    {
        public async Task<bool> DeleteStruct(HealthStruct s)
        {
            try
            {
                var structs = await GetStructs();
                structs.Remove(s);
                SaveStructs(structs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteStructs()
        {
            try
            {
                BlobCache.UserAccount.Invalidate("struct");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IList<HealthStruct>> GetStructs()
        {
            try
            {
                // need much test
                var s = await BlobCache.UserAccount.GetObject<IList<HealthStruct>>("struct") ;
                return s;
            }
            catch (Exception e)
            {
                Console.WriteLine("struct Services : error gettings struct list");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<bool> SaveStruct(HealthStruct s)
        {
            try
            {
                var structs =await GetStructs();
                structs.Add(s);
                SaveStructs(structs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveStructs(IList<HealthStruct> s)
        {
            try
            {
                BlobCache.UserAccount.Invalidate("struct");
                BlobCache.UserAccount.InsertObject("struct", s);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("struct Services : error saving structs list");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}
