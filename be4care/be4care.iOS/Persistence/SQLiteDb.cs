using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using be4care.Persistence;
using Foundation;
using SQLite;
using UIKit;

namespace be4care.iOS.Persistence
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "be4care.db3");
            return new SQLiteAsyncConnection(path);

        }
    }
}