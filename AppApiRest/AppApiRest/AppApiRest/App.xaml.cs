using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppApiRest.Services;

//using Plugin.LocalNotifications;

namespace AppApiRest
{
    public partial class App : Application
    {

        public static ServicesManager Manager { get; set; }

        public App()
        {
            Manager = new ServicesManager(new RestService());

            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {


            //Device.StartTimer(TimeSpan.FromSeconds(10), (Func<bool>)(() =>
            //{

            //    CrossLocalNotifications.Current.Show("No olvidar", "Notificacion con  Device Starter"
            //        + DateTime.Now.ToLongTimeString());

            //    return true;
            //}));

        }

        protected override void OnResume()
        {
        }
    }
}
