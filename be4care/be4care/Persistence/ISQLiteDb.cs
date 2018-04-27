using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Persistence
{
    public  interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
