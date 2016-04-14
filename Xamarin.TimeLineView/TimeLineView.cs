using System;
using Android.Views;
using Android.Content;
using Android.Util;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace Xamarin.TimeLineView
{
	public class TimeLine:View
	{

		private Drawable marker;
		private Drawable startLine;
		private Drawable endLine;
		private int markerSize;
		private int lineSize;

		private Rect bounds;


		public Drawable Marker {
			set { 
				marker = value;
				InitDrawable ();
			}
		}

		public Drawable StartLine {
			set { 
				startLine = value;
				InitDrawable ();
			}
		}

		public Drawable EndLine {
			set { 
				endLine = value;
				InitDrawable ();
			}
		}


		public int MarkerSize {
			set { 
				markerSize = value;
				InitDrawable ();
			}
		}

		public int LineSize {
			set { 
				lineSize = value;
				InitDrawable ();
			}
		}



		public TimeLine (Context context, IAttributeSet attrs) : base (context, attrs)
		{

			TypedArray typedArray = context.ObtainStyledAttributes (attrs, Resource.Styleable.timeline_style);
			marker = typedArray.GetDrawable (Resource.Styleable.timeline_style_marker);
			startLine = typedArray.GetDrawable (Resource.Styleable.timeline_style_line);
			endLine = typedArray.GetDrawable (Resource.Styleable.timeline_style_line);
			markerSize = typedArray.GetDimensionPixelSize (Resource.Styleable.timeline_style_marker_size, 25);
			lineSize = typedArray.GetDimensionPixelSize (Resource.Styleable.timeline_style_line_size, 2);
			typedArray.Recycle ();

			if (marker == null) {
				marker = Resources.GetDrawable (Resource.Drawable.marker);
			}

		}


		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			base.OnMeasure (widthMeasureSpec, heightMeasureSpec);
			//Width measurements of the width and height and the inside view of child controls
			var width = markerSize + PaddingLeft + PaddingRight;
			var height = markerSize + PaddingTop + PaddingBottom;

			// Width and height to determine the final view through a systematic approach to decision-making
			int widthSize = ResolveSizeAndState (width, widthMeasureSpec, 0);
			int heightSize = ResolveSizeAndState (height, heightMeasureSpec, 0);

			SetMeasuredDimension (widthSize, heightSize);
			InitDrawable ();
		}

		protected override void OnSizeChanged (int w, int h, int oldw, int oldh)
		{
			base.OnSizeChanged (w, h, oldw, oldh);
			InitDrawable ();
		}


		private void InitDrawable ()
		{
			int pLeft = PaddingLeft;
			int pRight = PaddingRight;
			int pTop = PaddingTop;
			int pBottom = PaddingBottom;

			int width = Width;// Width of current custom view
			int height = Height;

			int cWidth = width - pLeft - pRight;// Circle width
			int cHeight = height - pTop - pBottom;

			int markSize = Math.Min (markerSize, Math.Min (cWidth, cHeight));

			if (marker != null) {
				marker.SetBounds (pLeft, pTop, pLeft + markSize, pTop + markSize);
				bounds = marker.Bounds;
			}

			int centerX = bounds.CenterX ();
			int lineLeft = centerX - (lineSize >> 1);
			if (startLine != null) {
				startLine.SetBounds (lineLeft, 0, lineSize + lineLeft, bounds.Top);
			}

			if (endLine != null) {
				endLine.SetBounds (lineLeft, bounds.Bottom, lineSize + lineLeft, height);
			}

		}

		protected override void OnDraw (Android.Graphics.Canvas canvas)
		{
			base.OnDraw (canvas);
			if (marker != null) {
				marker.Draw (canvas);
			}

			if (startLine != null) {
				startLine.Draw (canvas);
			}
			if (endLine != null) {
				endLine.Draw (canvas);
			}
		}


		public void InitLine (LineType viewType)
		{

			if (viewType == LineType.Begin) {
				startLine = null;
			} else if (viewType == LineType.End) {
				endLine = null;
			} else if (viewType == LineType.Onlyone) {
				startLine = null;
				endLine = null;
			}

			InitDrawable ();
		}

		public static LineType GetTimeLineViewType (int position, int total_size)
		{
			if (total_size == 1) {
				return LineType.Onlyone;
			} else if (position == 0) {
				return LineType.Begin;
			} else if (position == total_size - 1) {
				return LineType.End;
			} else {
				return LineType.Normal;
			}
		}
	}


	public enum LineType
	{
		Normal,
		Begin,
		End,
		Onlyone
	}
}

