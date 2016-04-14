# Xamarin.TimeLineView

Xamarin.Android 时间轴效果，改写https://github.com/vipulasri/Timeline-View。

实现效果：
![image](https://github.com/MyueX/Xamarin.TimeLineView/blob/master/QQ20160414-0.png)

使用结合RecyclerView使用，在item定义中添加代码：
```xml
<Xamarin.TimeLineView.TimeLine
        android:id="@+id/time_marker"
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        android:paddingTop="30dp"
        android:paddingBottom="30dp"
        android:paddingLeft="10dp"
        android:paddingRight="10dp"
        app:marker_size="25dp"
        app:line_size="2dp"
        app:line="@color/colorPrimary" />
```

CS代码：
RecyclerView.ViewHolder 的初始化方法中调用
```C#
public class TimeLineViewHolder:RecyclerView.ViewHolder
	{

		public TimeLineViewHolder (View itemView, int viewType) : base (itemView)
		{
			//省略代码
			
			TimelineView = itemView.FindViewById<TimeLine> (Resource.Id.time_marker);
			TimelineView.InitLine ((LineType)viewType);
		}

	}
```
初始化线条。


重写RecyclerView.Adapter的GetItemViewType (int position)方法
```C#
public override int GetItemViewType (int position)
		{
			return (int)TimeLine.GetTimeLineViewType (position, ItemCount);
		}
```
在 OnCreateViewHolder 方法中传入viewType
```C#
public override RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
		{
			View view = View.Inflate (parent.Context, Resource.Layout.item_timeline, null);
			return new TimeLineViewHolder (view, viewType);
		}
```
