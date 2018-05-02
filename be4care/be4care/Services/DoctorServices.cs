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
                BlobCache.UserAccount.Invalidate("doctors");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IList<Doctor>> GetDoctors()
        {
            try
            {
                // need much test
                var docs = await BlobCache.UserAccount.GetObject<IList<Doctor>>("doctors") ;
                return docs  ;
            }
            catch (Exception e)
            {
                Console.WriteLine("Doctor Services : error gettings doctors list");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<bool> SaveDoctor(Doctor doc)
        {
            try
            {
                var docs = await GetDoctors();
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
                BlobCache.UserAccount.Invalidate("doctors");
                BlobCache.UserAccount.InsertObject("doctors", docs);
                return true;
            }catch(Exception e)
            {
                Console.WriteLine("Doctor Services : error saving doctors list");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}
