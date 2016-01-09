using System;
using Paint.Droid.Views.PaintView;
using Paint.Views.PaintView;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(Paint.Views.PaintView.PaintView), typeof(PaintViewRenderer))]
namespace Paint.Droid.Views.PaintView
{
    public class PaintViewRenderer : ViewRenderer<Paint.Views.PaintView.PaintView, Android.Views.View>
    {
        #region Private Members

        private Paint.Views.PaintView.PaintView _formsView;

        #endregion

        // ---------------------------------------------------------

        #region Overrides

        protected override void OnElementChanged(ElementChangedEventArgs<Paint.Views.PaintView.PaintView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                //TODO: Clean up...
            }

            if (e.NewElement != null)
            {
                _formsView = e.NewElement;
            }
        }

        #endregion
    }
}