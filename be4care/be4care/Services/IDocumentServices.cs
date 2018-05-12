using be4care.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace be4care.Services
{
    interface IDocumentServices
    {
        bool SaveDocuments(IList<Document> docs);
        Task<bool> SaveDocument(Document doc);
        Task<IList<Document>> GetDocuments();
        Task<bool> DeleteDocument(Document doc);
        bool DeleteDocuments();
        Task<bool> UpdateDocument(Document d);
    }
}
