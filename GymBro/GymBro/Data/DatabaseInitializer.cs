using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymBro.Models;
using SQLite;

namespace GymBro.Data
{
    public static class Database
    {
        public static void Initialise(SQLiteConnection connection)
        {
            connection.CreateTable<Exercise>();
            connection.CreateTable<Person>();
            connection.CreateTable<Routine>();

            Int32 personCount = connection.ExecuteScalar<Int32>("Select count(*) from Person");
            if (personCount == 0)
            {
                connection.Insert(new Person { Id = 1, FullName = "Kim Crowe", DisplayName = "Kim", Initials = "KC", ColorCode = "#C120FF" });
                connection.Insert(new Person { Id = 1, FullName = "Vincent Crowe", DisplayName = "Vin", Initials = "VC", ColorCode = "#237EFF" });
            }
        }
    }
}
