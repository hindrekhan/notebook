using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace notebook
{
    [Activity(Label = "NoteActivity")]
    public class NoteActivity : Activity
    {
        DatabaseService databaseService;
        Note note;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_note);

            note = JsonConvert.DeserializeObject<Note>(Intent.GetStringExtra("note"));
            databaseService = new DatabaseService();
            databaseService.CreateDatabase();

            var title = FindViewById<TextView>(Resource.Id.title);
            var content = FindViewById<TextView>(Resource.Id.content);
            var save = FindViewById<Button>(Resource.Id.save);
            var delete = FindViewById<Button>(Resource.Id.delete);

            title.Text = note.Title;
            content.Text = note.Content;

            save.Click += Save_Click;
            delete.Click += Delete_Click;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var content = FindViewById<TextView>(Resource.Id.content);

            note.Content = content.Text;
            databaseService.UpdateNote(note);
            Finish();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            databaseService.RemoveNote(note);
            Finish();
        }
    }
}