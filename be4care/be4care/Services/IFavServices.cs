using be4care.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace be4care.Services
{
    interface IFavServices
    {
        Task<bool> AddFavDoctorAsync(Doctor d);
        Task<bool> AddFavHealthStructAsync(HealthStruct h);
        Task<bool> AddFavDocumentAsync(Document doc);
        Task<bool> DeleteFavsAsync();
        Task<bool> DeleteFavDoctorAsync(Doctor d);
        Task<bool> DeleteFavHealthStructAsync(HealthStruct h);
        Task<bool> DeleteFavDocumentAsync(Document doc);
        Task<IList<Doctor>> GetFavDoctorsAsync();
        Task<IList<HealthStruct>> GetFavHealthStructAsync();
        Task<IList<Document>> GetFavDocumentAsync();


    }
}
