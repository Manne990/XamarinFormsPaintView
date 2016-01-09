using System;

using Xamarin.Forms;

namespace Paint.Views.PaintView
{
    public class PaintView : Image
    {
        public PaintView()
        {
            
        }

        public static readonly BindableProperty PaintColorProperty = BindableProperty.Create("PaintColor", typeof(Color), typeof(PaintView), Color.Black);
        public Color PaintColor
        {
            get { return (Color)GetValue(PaintColorProperty); }
            set { SetValue(PaintColorProperty, value); }
        }

        public static readonly BindableProperty LineWidthProperty = BindableProperty.Create("LineWidth", typeof(float), typeof(PaintView), 10.0f);
        public float LineWidth
        {
            get { return (float)GetValue(LineWidthProperty); }
            set { SetValue(LineWidthProperty, value); }
        }

        public void Clear()
        {
            this.Source = null;
        }
    }
}