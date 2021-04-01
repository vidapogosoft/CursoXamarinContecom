using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppApiRest.Services;

namespace AppApiRest
{
    public partial class App : Application
    {

        public static ServicesManager Manager { get; set; }

        public App()
        {
            Manager = new ServicesManager(new RestService());

            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
