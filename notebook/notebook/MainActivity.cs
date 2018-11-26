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
            databaseService.CreateTableWithData();
            var notes = databaseService.GetAllNotes();

            var listView = FindViewById<ListView>(Resource.Id.listView1);
            listView.Adapter = new CustomAdapter(this, notes);

            var button = FindViewById<Button>(Resource.Id.button1);
            button.Click += Button_Click;
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            Note note = new Note();
            var input = FindViewById<EditText>(Resource.Id.input);

            note.Content = input.Text;

            databaseService.AddNote(note);

            var notes = databaseService.GetAllNotes();

            var listView = FindViewById<ListView>(Resource.Id.listView1);
            listView.Adapter = new CustomAdapter(this, notes);
        }
    }
}