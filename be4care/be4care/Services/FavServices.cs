using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive.Linq;
using be4care.Models;
using System.Threading.Tasks;
using Akavache;

namespace be4care.Services
{
    class FavServices : IFavServices
    {
        public async Task<bool> AddFavDoctorAsync(Doctor d)
        {
            try
            {
                var docs = await GetFavDoctorsAsync();
                if (docs == null)
                    docs = new List<Doctor>();
                docs.Add(d);
                try
                {
                    
                    await BlobCache.InMemory.InsertObject("favDoctor", docs);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("fav service , error adding a new favorite item");
                    Console.WriteLine(e.StackTrace);
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddFavDocumentAsync(Document doc)
        {
            try
            {
                var docs = await GetFavDocumentAsync();
                if (docs == null)
                    docs = new List<Document>();
                docs.Add(doc);
                try
                {

                    await BlobCache.InMemory.InsertObject("favDocument", docs);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("fav service , error adding a new favorite item");
                    Console.WriteLine(e.StackTrace);
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddFavHealthStructAsync(HealthStruct h)
        {
            try
            {
                var docs = await GetFavHealthStructAsync();
                if (docs == null)
                    docs = new List<HealthStruct>();
                docs.Add(h);
                try
                {

                    await BlobCache.InMemory.InsertObject("favHstruct", docs);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("fav service , error adding a new favorite item");
                    Console.WriteLine(e.StackTrace);
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> DeleteFavDoctorAsync(Doctor d)
        {
            try
            {
                var docs = await GetFavDoctorsAsync();
                if (docs == null)
                    return true;
                docs.Remove(d);
                await BlobCache.InMemory.InsertObject("favDoctor", docs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteFavDocumentAsync(Document doc)
        {
            try
            {
                var docs = await GetFavDocumentAsync();
                if (docs == null)
                    return true;
                docs.Remove(doc);
                await BlobCache.InMemory.InsertObject("favDocument", docs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteFavHealthStructAsync(HealthStruct h)
        {
            try
            {
                var docs = await GetFavHealthStructAsync();
                if (docs == null)
                    return true;
                docs.Remove(h);
                await BlobCache.InMemory.InsertObject("favHstruct", docs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteFavsAsync()
        {
            try
            {
                await BlobCache.InMemory.Invalidate("favDocument");
                await BlobCache.InMemory.Invalidate("favHstruct");
                await BlobCache.InMemory.Invalidate("favDoctor");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IList<Doctor>> GetFavDoctorsAsync()
        {
            try
            {
                // need much test
                var docs = await BlobCache.InMemory.GetObject<IList<Doctor>>("favDoctor");
                return docs;
            }
            catch (Exception e)
            {
                Console.WriteLine("fav service , error getting favorite doctor");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<IList<Document>> GetFavDocumentAsync()
        {
            try
            {
                // need much test
                var docs = await BlobCache.InMemory.GetObject<IList<Document>>("favDocument");
                return docs;
            }
            catch (Exception e)
            {
                Console.WriteLine("fav service , error getting favorite documents");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<IList<HealthStruct>> GetFavHealthStructAsync()
        {
            try
            {
                // need much test
                var docs = await BlobCache.InMemory.GetObject<IList<HealthStruct>>("favHstruct");
                return docs;
            }
            catch (Exception e)
            {
                Console.WriteLine("fav service , error getting favorite hstruct");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
