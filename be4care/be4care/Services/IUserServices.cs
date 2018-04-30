using be4care.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace be4care.Services
{
    interface IUserServices
    {
        void SaveUser(User user);
        Task<User> GetUser();
        bool DeleteUser();
    }
}
