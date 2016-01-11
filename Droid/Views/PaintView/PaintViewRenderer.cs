using System;
using System.Collections.Generic;
using System.Linq;
using Android.Graphics;
using Android.Support.V4.View;
using Android.Views;
using Paint.Droid.Common;
using Paint.Droid.Extensions;
using Paint.Droid.Views.PaintView;
using Paint.Views.PaintView;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(Paint.Views.PaintView.PaintView), typeof(PaintViewRenderer))]
namespace Paint.Droid.Views.PaintView
{
    public class PaintViewRenderer : ImageRenderer
    {
        #region Private Members

        private Paint.Views.PaintView.PaintView _formsView;
        private List<int> _pointerIds;
        private List<PaintPointer> _lastPointers;
        private Android.Graphics.Paint _currentPaint;
        private Bitmap _bitmap;
        private Canvas _canvas;
        private float _deviceDensity;

        #endregion

        // ---------------------------------------------------------

        #region Overrides

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                _formsView = (Paint.Views.PaintView.PaintView)e.NewElement;

                _currentPaint = new Android.Graphics.Paint();
                _currentPaint.Dither = true;
                _currentPaint.SetStyle(Android.Graphics.Paint.Style.Stroke);
                _currentPaint.StrokeJoin = Android.Graphics.Paint.Join.Round;
                _currentPaint.StrokeCap = Android.Graphics.Paint.Cap.Round;

                _deviceDensity = AndroidDevice.DisplayMetrics.Density > 1 ? AndroidDevice.DisplayMetrics.Density : 1;
            }
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            // Create the bitmap and canvas when we have a size
            _bitmap = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);
            _canvas = new Canvas(_bitmap);

            base.OnSizeChanged(w, h, oldw, oldh);
        }

        public override bool OnTouchEvent(Android.Views.MotionEvent e)
        {
            if(e.Action == MotionEventActions.Down)
            {
                // Set values from settings
                _currentPaint.Color = _formsView.PaintColor.ToAndroid();
                _currentPaint.StrokeWidth = _formsView.LineWidth * _deviceDensity;

                // Init storage for pointers and historical coords
                _pointerIds = new List<int>();
                _lastPointers = new List<PaintPointer>();

                // Loop all pointers
                for(int i = 0; i < e.PointerCount; i++)
                {
                    // Get and store the pointer
                    var pointerId = e.GetPointerId(i);

                    // Get the pointer coords
                    _pointerIds.Add(pointerId);

                    int pointerIndex = e.FindPointerIndex(pointerId);

                    var currentPoint = new MotionEvent.PointerCoords();

                    e.GetPointerCoords(pointerIndex, currentPoint);

                    // Store the coord in the historical list
                    _lastPointers.Add(new PaintPointer() { PointerId = pointerId, Coords = currentPoint });

                    // Draw the line
                    DrawLine(currentPoint, currentPoint);
                }
            }

            if(e.Action == MotionEventActions.Move)
            {
                // Clone the historical coords
                var lastPointers = _lastPointers.Clone();

                // Reset the historical coords
                _lastPointers = new List<PaintPointer>();

                // Loop all pointers
                foreach(var pointerId in _pointerIds)
                {
                    // Get the pointer coords
                    int pointerIndex = e.FindPointerIndex(pointerId);

                    var currentPoint = new MotionEvent.PointerCoords();
                    e.GetPointerCoords(pointerIndex, currentPoint);

                    // Get the last (historical) pointer coords
                    var lastPoint = lastPointers.FirstOrDefault(l => l.PointerId == pointerId).Coords;

                    if(lastPoint == null)
                    {
                        lastPoint = currentPoint;
                    }

                    // Store the coord in the historical list
                    _lastPointers.Add(new PaintPointer() { PointerId = pointerId, Coords = currentPoint });

                    // Draw the line
                    DrawLine(lastPoint, currentPoint);
                }
            }

            return true;
        }

        #endregion

        // ---------------------------------------------------------

        #region Private Methods

        private void DrawLine(MotionEvent.PointerCoords point1, MotionEvent.PointerCoords point2)
        {
            // Draw the line
            _canvas.DrawLine(point1.X - 0.5f, point1.Y - 0.5f, point2.X, point2.Y, _currentPaint);

            // Set the bitmap to the ImageView
            this.Control.SetImageBitmap(_bitmap);
        }

        #endregion

        // ---------------------------------------------------------

        #region Private Classes

        private class PaintPointer : ICloneable
        {
            #region ICloneable implementation

            public object Clone()
            {
                var clone = new PaintPointer();

                clone.PointerId = this.PointerId;
                clone.Coords = this.Coords;

                return clone;
            }

            #endregion

            public int PointerId { get; set; }
            public MotionEvent.PointerCoords Coords { get; set; }
        }

        #endregion
    }
}