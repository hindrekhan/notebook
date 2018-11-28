using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;

namespace notebook
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        DatabaseService databaseService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            databaseService = new DatabaseService();
            databaseService.CreateDatabase();

            var button = FindViewById<Button>(Resource.Id.button);
            button.Click += Button_Click;
        }

        protected override void OnPostResume()
        {
            base.OnPostResume();

            var notes = databaseService.GetAllNotes();

            var listView = FindViewById<ListView>(Resource.Id.listView1);
            listView.Adapter = new CustomAdapter(this, notes, databaseService);
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            Note note = new Note();
            var inputTitle = FindViewById<EditText>(Resource.Id.inputTitle);
            var inputContent = FindViewById<EditText>(Resource.Id.inputContent);

            note.Title = inputTitle.Text;
            note.Content = inputContent.Text;

            databaseService.AddNote(note);

            var notes = databaseService.GetAllNotes();

            var listView = FindViewById<ListView>(Resource.Id.listView1);
            listView.Adapter = new CustomAdapter(this, notes, databaseService);
        }
    }
}