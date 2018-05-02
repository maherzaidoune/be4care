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

        bool AddDoctor(Doctor d);

        Task<IList<HealthStruct>> GetHealthStructs();

        bool AddHealthStruct(HealthStruct s);

        Task<string> Analyse(User user);

        Task<string> Analyse(string Url);

        bool GetAccessToken(string username, string password);

        User GetMyProfile();

        bool Inscription(string data, string pass);

        bool UpdateProfile(User user);
    }
}
