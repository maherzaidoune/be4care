using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive.Linq;
using be4care.Models;
using System.Threading.Tasks;

namespace be4care.Services
{
    class FavServices : IFavServices
    {
        public bool AddFavDoctor(Doctor d)
        {
            throw new NotImplementedException();
        }

        public bool AddFavDocument(Document doc)
        {
            throw new NotImplementedException();
        }

        public bool AddFavHealthStruct(HealthStruct h)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFavDoctor(Doctor d)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFavDocument(Document doc)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFavHealthStruct(HealthStruct h)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFavs()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Doctor>> GetFavDoctors()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Document>> GetFavDocument()
        {
            throw new NotImplementedException();
        }

        public Task<IList<HealthStruct>> GetFavHealthStruct()
        {
            throw new NotImplementedException();
        }
    }
}
