using be4care.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace be4care.Services
{
    interface IRestServices
    {
        bool Init();



        Task<IList<Document>> GetDocumentsAsync();

        Task<IList<Doctor>> GetDoctorsAsync();

        Task<IList<Doctor>> GetAllDoctors();

       Task<IList<HealthStruct>> GetAllHstruct();

        Task<bool> AddDoctor(Doctor d);

        Task<bool> AddDoctorFromDB(string id);
        Task<bool> AddHStructFromDb(string id);

        bool addDocument(Document d);

        Task<IList<HealthStruct>> GetHealthStructs();

        Task<bool> AddHealthStruct(HealthStruct s);

        Task<string> Upload(User user);

        Task<string> Analyse(string Url);

        bool GetAccessToken(string username, string password);

        User GetMyProfile();

        bool Inscription(string data, string pass);

        bool UpdateProfile(User user);

        bool Disconnect();

        bool Delete();
    }
}
