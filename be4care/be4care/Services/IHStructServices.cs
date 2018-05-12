using be4care.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace be4care.Services
{
    public  interface IHStructServices
    {
        bool SaveStructs(IList<HealthStruct> s);
        Task<bool> SaveStruct(HealthStruct s);
        Task<IList<HealthStruct>> GetStructs();
        Task<bool> DeleteStruct(HealthStruct s);
        bool DeleteStructs();
        bool saveAllHstruct(IList<HealthStruct> s);
        Task<IList<HealthStruct>> GetAllHstrcts();
        Task<bool> UpdateHStructAsync(HealthStruct h);
    }
}
