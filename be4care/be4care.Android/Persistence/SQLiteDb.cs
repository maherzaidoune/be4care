using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using be4care.Persistence;
using SQLite;

namespace be4care.Droid.Persistence
{
    public  class SQLiteDb: ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "be4care.db3");
            return new SQLiteAsyncConnection(path);

        }
    }
}