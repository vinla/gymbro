using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace GymBro.Data
{
    public interface IConnectionProvider
    {
        SQLiteConnection GetConnection();
    }
}
