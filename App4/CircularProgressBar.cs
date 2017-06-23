using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;

namespace App4
{
    public class CircularProgressBar : View
    {
        private RectF rectF;
        private Paint paintCircle = new Paint();
        private Paint paintText = new Paint();
        private float sweepAngle = 360f;
        private int seconds;
        private TimeSpan time;
        private string TimeString = "";

        public CircularProgressBar(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public CircularProgressBar(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        private void Initialize()
        {
            
        }

        public void ChangeSweepAngle(float angle)
        {
            this.sweepAngle = angle ;
        }

        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
        }

        public void SetTime(int seconds)
        {
            this.seconds = seconds;
            this.time = TimeSpan.FromSeconds(seconds);
            this.TimeString = time.ToString(@"mm\:ss");
        }

        protected override void OnDraw(Canvas canvas)
        {
            var metrics = Resources.DisplayMetrics;
            var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
            var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);
            paintCircle.SetStyle(Paint.Style.Stroke);
            RectF rectF = new RectF(20, 20, this.Width-20, this.Width-20);
            paintCircle.AntiAlias = true;
            paintCircle.Color = Color.Rgb(124, 249, 0);
            paintCircle.StrokeWidth = 10;

            this.rectF = rectF;

            paintText.SetStyle(Paint.Style.FillAndStroke);
            paintText.StrokeWidth = 1;
            paintText.TextSize = this.Width / 4;
            paintText.AntiAlias = true;
            paintText.SetTypeface(Typeface.Create("Arial", TypefaceStyle.Italic));
            paintText.Color = Color.Rgb(124, 249, 0);
            paintText.TextAlign = Paint.Align.Center;

            canvas.DrawArc(this.rectF, 270, - this.sweepAngle, false, this.paintCircle);
            canvas.DrawText(TimeString, this.Width/2, this.Height/2 + 50, this.paintText);
        }
    }
}