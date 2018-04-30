using Akavache.Sqlite3;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care
{
    [Preserve]
    public static class LinkerPreserve
    {
        static LinkerPreserve()
        {
            throw new Exception(typeof(SQLitePersistentBlobCache).FullName);
        }
    }


    public class PreserveAttribute : Attribute
    {
    }
}
