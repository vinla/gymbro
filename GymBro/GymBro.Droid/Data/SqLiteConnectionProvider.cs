using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SQLite;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly:Xamarin.Forms.Dependency(typeof(GymBro.Droid.Data.SqLiteConnectionProvider))]
namespace GymBro.Droid.Data
{
    public class SqLiteConnectionProvider : GymBro.Data.IConnectionProvider
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "GymBroDb_V1.db3";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = System.IO.Path.Combine(documentsPath, sqliteFilename);            
            return new SQLiteConnection(path);
        }
    }
}