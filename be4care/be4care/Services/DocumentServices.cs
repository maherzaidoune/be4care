﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using be4care.Models;
using System.Reactive.Linq;


namespace be4care.Services
{
    class DocumentServices : IDocumentServices
    {
        public async Task<bool> DeleteDocument(Document doc)
        {
            try
            {
                var docs = await GetDocuments();
                if (docs == null || docs.Count == 0)
                    return false;
                docs.Remove(doc);
                SaveDocuments(docs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteDocuments()
        {
            try
            {
                BlobCache.InMemory.Invalidate("documents");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IList<Document>> GetDocuments()
        {
            try
            {
                // need much test
                var docs = await BlobCache.InMemory.GetObject<IList<Document>>("documents");
                return docs;
            }
            catch (Exception e)
            {
                Console.WriteLine("Documents Services : error gettings doctors list");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<bool> SaveDocument(Document doc)
        {
            try
            {
                var docs = await GetDocuments();
                if (docs == null)
                    return false;
                docs.Add(doc);
                SaveDocuments(docs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveDocuments(IList<Document> docs)
        {
            try
            {
                BlobCache.InMemory.Invalidate("documents");
                BlobCache.InMemory.InsertObject("documents", docs);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Documents Services : error saving doctors list");
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    
    }
}
