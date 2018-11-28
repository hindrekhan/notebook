using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace notebook
{
    class CustomAdapter : BaseAdapter<Note>
    {
        List<Note> items;
        Activity context;
        DatabaseService databaseService;
        bool initialized;

        public CustomAdapter(Activity context, List<Note> items, DatabaseService databaseService) : base()
        {
            this.context = context;
            this.items = items;
            this.databaseService = databaseService;
        }

        public override Note this[int position]
        {
            get { return items[position]; }
        }

        public override int Count { get { return items.Count; } }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomRow, null);
            }

            var title = view.FindViewById<TextView>(Resource.Id.title);
            title.Text = items[position].Title;

            view.Tag = position;
            view.Click -= View_Click;
            view.Click += View_Click;
            initialized = true;

            return view;
        }

        private void View_Click(object sender, EventArgs e)
        {
            var position = (int)((View)sender).Tag;

            Intent intent = new Intent(context, typeof(NoteActivity));
            intent.PutExtra("note", JsonConvert.SerializeObject(items[position]));
            context.StartActivity(intent);
        }
    }
}