using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Xamarin.TimeLineView;
using System.Collections.Generic;

namespace Xamarin.TimeLineViewTest
{
	[Activity (Label = "Xamarin.TimeLineViewTest", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{

		private RecyclerView recyclerView;

		private TimeLineAdapter timeLineAdapter;

		private List<TimeLineModel> mDataList = new List<TimeLineModel> ();

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			recyclerView = FindViewById<RecyclerView> (Resource.Id.recyclerView);
			LinearLayoutManager linearLayoutManager = new LinearLayoutManager (this);
			recyclerView.SetLayoutManager (linearLayoutManager);

			InitView ();
		}


		private void InitView ()
		{

			for (int i = 0; i < 30; i++) {
				TimeLineModel model = new TimeLineModel ();
				model.Name = "Random" + i;
				model.Age = i;
				mDataList.Add (model);
			}

			timeLineAdapter = new TimeLineAdapter (mDataList);
			recyclerView.SetAdapter (timeLineAdapter);
		}
	}


	public class TimeLineModel
	{
		public string Name {
			get;
			set;
		}

		public int Age {
			get;
			set;
		}
	}

	public class TimeLineViewHolder:RecyclerView.ViewHolder
	{
		public TextView Name {
			get;
			set;
		}

		public TimeLine TimelineView {
			get;
			set;
		}

		public TimeLineViewHolder (View itemView, int viewType) : base (itemView)
		{
			Name = itemView.FindViewById<TextView> (Resource.Id.tx_name);
			TimelineView = itemView.FindViewById<TimeLine> (Resource.Id.time_marker);
			TimelineView.InitLine ((LineType)viewType);
		}

	}


	public class TimeLineAdapter:RecyclerView.Adapter
	{

		private List<TimeLineModel> data;

		public TimeLineAdapter (List<TimeLineModel> feedList)
		{
			data = feedList;
		}


		public override int GetItemViewType (int position)
		{
			return (int)TimeLine.GetTimeLineViewType (position, ItemCount);
		}


		public override RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
		{
			View view = View.Inflate (parent.Context, Resource.Layout.item_timeline, null);
			return new TimeLineViewHolder (view, viewType);
		}

		public override void OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
		{
			TimeLineModel timeLineModel = data [position];

			var timeLineHolder = holder as TimeLineViewHolder;

			timeLineHolder.Name.Text = "name：" + timeLineModel.Name + "    age：" + timeLineModel.Age;
		}


		public override int ItemCount {
			get{ return data.Count; }
		}


	}

}


