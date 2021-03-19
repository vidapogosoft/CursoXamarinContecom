using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GridLayout.Views;

namespace GridLayout
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();

            MainPage = new Home();

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
