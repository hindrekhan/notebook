﻿using System;
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

namespace notebook
{
    class CustomAdapter : BaseAdapter<Note>
    {
        List<Note> items;
        Activity context;

        public CustomAdapter(Activity context, List<Note> items) : base()
        {
            this.context = context;
            this.items = items;
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
            view.Click += View_Click;

            return view;
        }

        private void View_Click(object sender, EventArgs e)
        {
            var position = (int)((View)sender).Tag;

            Intent intent = new Intent(context, typeof(NoteActivity));
            intent.PutExtra("pos", position);

            context.StartActivity(intent);
        }
    }
}