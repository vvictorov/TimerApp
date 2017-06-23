using Android.App;
using Android.Widget;
using Android.OS;
using System.Timers;
using System;
using Android.Views;

namespace App4
{
    [Activity(Label = "App4", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected float sweepAngle = 360f;
        protected CircularProgressBar progressBarView;
        protected int timeSeconds;
        protected int currentSeconds;
        protected Timer CountDownTimer = new Timer(10);
        protected Timer SecondsTimer = new Timer(1000);

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            CircularProgressBar progressBarView = FindViewById<CircularProgressBar>(Resource.Id.progressBar);
            this.progressBarView = progressBarView;
            Button buttonStart = FindViewById<Button>(Resource.Id.buttonStart);
            Button buttonStop = FindViewById<Button>(Resource.Id.buttonStop);

            buttonStart.Click += delegate {
                EditText time = FindViewById<EditText>(Resource.Id.time);
                if(time.Text != "")
                {
                    buttonStart.Enabled = false;
                    buttonStop.Enabled = true;
                    StartCountdown();
                }
            };
         
            buttonStop.Click += delegate {
                buttonStart.Enabled = true;
                buttonStop.Enabled = false;
                StopCountdown();
            };
        }

        protected void StartCountdown()
        {
            EditText time = FindViewById<EditText>(Resource.Id.time);
            timeSeconds = Int32.Parse(time.Text);
            currentSeconds = timeSeconds;
            progressBarView.SetTime(timeSeconds);
            CountDownTimer.Elapsed += OnTimedEvent;
            CountDownTimer.AutoReset = true;
            CountDownTimer.Enabled = true;
            CountDownTimer.Start();
            SecondsTimer.Elapsed += OnSecondsChanged;
            SecondsTimer.AutoReset = true;
            SecondsTimer.Enabled = true;
            SecondsTimer.Start();
        }

        protected void StopCountdown()
        {
            CountDownTimer.Stop();
            SecondsTimer.Stop();
            CountDownTimer.Elapsed -= OnTimedEvent;
            SecondsTimer.Elapsed -= OnSecondsChanged;
            sweepAngle = 360f;
            RunOnUiThread(() =>
            {
                progressBarView.ChangeSweepAngle(0);
                progressBarView.Invalidate();
            });
        }

        protected void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if(sweepAngle > 0)
            {
                sweepAngle = sweepAngle - (360f/((float)timeSeconds*100));
                RunOnUiThread(() =>
                {
                    progressBarView.ChangeSweepAngle(sweepAngle);
                    progressBarView.Invalidate();
                });
            }
        }

        protected void OnSecondsChanged(Object source, ElapsedEventArgs e)
        {
            if (currentSeconds > 0)
            {
                RunOnUiThread(() =>
                {
                    progressBarView.SetTime(currentSeconds);
                    progressBarView.Invalidate();
                });
                currentSeconds--;
            }
        }
    }
}

