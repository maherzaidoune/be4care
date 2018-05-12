using be4care.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace be4care.Services
{
    public interface IDoctorServices
    {
        bool SaveDoctors(IList<Doctor> docs);
        Task<bool> SaveDoctor(Doctor doc);
        Task<IList<Doctor>> GetDoctors();
        Task<bool> DeleteDoctor(Doctor doctor);
        bool DeleteDoctors();
        bool saveAllDoctors(IList<Doctor> docs);
        Task<IList<Doctor>> GetAllDoctors();
        Task<bool> UpdateDoctor(Doctor d);
    }
}
