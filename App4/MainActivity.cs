using Android.App;
using Android.Widget;
using Android.OS;
using System.Timers;
using System;
using Android.Views;
using Android.Content.PM;

namespace App4
{
    [Activity(Label = "Fitness Timer", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
    ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        protected float sweepAngle = 360f;
        protected CircularProgressBar progressBarView;
        protected int timeSeconds;
        protected int currentSeconds;
        protected int timerCounter = 0;
        protected Timer CountDownTimer = new Timer(10);

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
          
        }

        protected void StopCountdown()
        {
            CountDownTimer.Stop();
            
            CountDownTimer.Elapsed -= OnTimedEvent;
            
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
                timerCounter++;
                sweepAngle = sweepAngle - (360f/((float)timeSeconds*100));
                RunOnUiThread(() =>
                {
                    progressBarView.ChangeSweepAngle(sweepAngle);
                    progressBarView.Invalidate();
                });
                if(timerCounter % 100 == 0 && currentSeconds > 0)
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
}

