using be4care.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace be4care.Services
{
    interface IFavServices
    {
        bool AddFavDoctor(Doctor d);
        bool AddFavHealthStruct(HealthStruct h);
        bool AddFavDocument(Document doc);
        bool DeleteFavs();
        bool DeleteFavDoctor(Doctor d);
        bool DeleteFavHealthStruct(HealthStruct h);
        bool DeleteFavDocument(Document doc);
        Task<IList<Doctor>> GetFavDoctors();
        Task<IList<HealthStruct>> GetFavHealthStruct();
        Task<IList<Document>> GetFavDocument();
    }
}
