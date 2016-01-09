using System;
using CoreGraphics;
using Paint.iOS.Views.PaintView;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(Paint.Views.PaintView.PaintView), typeof(PaintViewRenderer))]
namespace Paint.iOS.Views.PaintView
{
    public class PaintViewRenderer : ImageRenderer
    {
        #region Private Members

        private Paint.Views.PaintView.PaintView _formsView;

        #endregion

        // ---------------------------------------------------------

        #region Overrides

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                //TODO: Clean up...
            }

            if (e.NewElement != null)
            {
                _formsView = (Paint.Views.PaintView.PaintView)e.NewElement;
            }
        }

        #endregion
    }
}