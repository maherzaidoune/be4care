using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace be4care.Services
{
    public interface ISqliteDb
    {
        SQLiteAsyncConnection GetConnection(); 
    }
}
