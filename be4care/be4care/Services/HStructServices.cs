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
                if (structs == null)
                    structs = new List<HealthStruct>();
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
                BlobCache.InMemory.Invalidate("struct");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IList<HealthStruct>> GetAllHstrcts()
        {
            try
            {
                // need much test
                var s = await BlobCache.InMemory.GetObject<IList<HealthStruct>>("allstruct");
                return s;
            }
            catch (Exception e)
            {
                Console.WriteLine("struct Services : error gettings struct list");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<IList<HealthStruct>> GetStructs()
        {
            try
            {
                // need much test
                var s = await BlobCache.InMemory.GetObject<IList<HealthStruct>>("struct") ;
                return s;
            }
            catch (Exception e)
            {
                Console.WriteLine("struct Services : error gettings struct list");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public bool saveAllHstruct(IList<HealthStruct> s)
        {
            try
            {
                BlobCache.InMemory.Invalidate("allstruct");
                BlobCache.InMemory.InsertObject("allstruct", s);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("struct Services : error saving structs list");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public async Task<bool> SaveStruct(HealthStruct s)
        {
            try
            {
                var structs =await GetStructs();
                if (structs == null)
                    structs = new List<HealthStruct>();
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
                BlobCache.InMemory.Invalidate("struct");
                BlobCache.InMemory.InsertObject("struct", s);
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
