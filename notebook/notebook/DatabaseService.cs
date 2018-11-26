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
using SQLite;

namespace notebook
{
    public class DatabaseService
    {
        SQLiteConnection db;

        public void CreateDatabase()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "mydatabas.db3");
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Note>();
        }

        public void AddNote(Note name)
        {
            db.Insert(name);
        }

        public List<Note> GetAllNotes()
        {
            var table = db.Table<Note>();
            return table.ToList();
        }
    }
}