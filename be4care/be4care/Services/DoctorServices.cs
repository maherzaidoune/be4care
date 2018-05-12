using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using be4care.Models;

namespace be4care.Services
{
    public class DoctorServices : IDoctorServices
    {
        public DoctorServices()
        {
             
        }
        public async Task<bool> DeleteDoctor(Doctor doc)
        {
            try
            {
                var docs = await GetDoctors();
                if (docs == null)
                    docs = new List<Doctor>();
                docs.Remove(doc);
                SaveDoctors(docs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteDoctors()
        {
            try
            {
                BlobCache.InMemory.Invalidate("doctors");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IList<Doctor>> GetAllDoctors()
        {
            try
            {
                // need much test
                var docs = await BlobCache.InMemory.GetObject<IList<Doctor>>("alldoctors");
                return docs;
            }
            catch (Exception e)
            {
                Console.WriteLine("Doctor Services : error gettings doctors list");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<IList<Doctor>> GetDoctors()
        {
            try
            {
                // need much test
                var docs = await BlobCache.InMemory.GetObject<IList<Doctor>>("doctors") ;
                return docs  ;
            }
            catch (Exception e)
            {
                Console.WriteLine("Doctor Services : error gettings doctors list");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public bool saveAllDoctors(IList<Doctor> docs)
        {
            try
            {
                BlobCache.InMemory.Invalidate("alldoctors");
                BlobCache.InMemory.InsertObject("alldoctors", docs);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Doctor Services : error saving doctors list");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public async Task<bool> SaveDoctor(Doctor doc)
        {
            try
            {
                var docs = await GetDoctors();
                if (docs == null)
                    docs = new List<Doctor>();
                docs.Add(doc);
                SaveDoctors(docs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public  bool SaveDoctors(IList<Doctor> docs)
        {
            try
            {
                BlobCache.InMemory.Invalidate("doctors");
                BlobCache.InMemory.InsertObject("doctors", docs);
                return true;
            }catch(Exception e)
            {
                Console.WriteLine("Doctor Services : error saving doctors list");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public async Task<bool> UpdateDoctor(Doctor d)
        {
            try
            {
                int pos = 0;
                var docs = await GetDoctors();
                if (docs == null)
                    return false;
                foreach(Doctor doctor in docs)
                {
                    if (doctor.id == d.id)
                    {
                        docs.Remove(doctor);
                        docs.Insert(pos, d);
                        break;
                    }
                    pos++;
                }
                SaveDoctors(docs);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
