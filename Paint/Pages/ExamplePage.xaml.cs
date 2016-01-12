using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Paint.Pages
{
    public partial class ExamplePage : ContentPage
    {
        public ExamplePage()
        {
            InitializeComponent();
        }

        private void BlackButtonClicked(object sender, EventArgs args)
        {
            paintView.PaintColor = Color.Black;
        }

        private void WhiteButtonClicked(object sender, EventArgs args)
        {
            paintView.PaintColor = Color.White;
        }

        private void RedButtonClicked(object sender, EventArgs args)
        {
            paintView.PaintColor = Color.Red;
        }

        private void GreenButtonClicked(object sender, EventArgs args)
        {
            paintView.PaintColor = Color.Green;
        }

        private void BlueButtonClicked(object sender, EventArgs args)
        {
            paintView.PaintColor = Color.Blue;
        }

        private void LineWidthValueChanged(object sender, ValueChangedEventArgs e)
        {
            paintView.LineWidth = (float)e.NewValue;
        }
    }
}