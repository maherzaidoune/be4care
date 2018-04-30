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

        string Analyse();

        string UploadPhoto(string Url);

        bool GetAccessToken(string username, string password);

        User GetMyProfile();

        bool Inscription(string data, string pass);

        bool UpdateProfile(User user);
    }
}
