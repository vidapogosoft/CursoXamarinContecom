using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MenuTabsMaestroDetalle
{
    public partial class App : Application
    {
        [Obsolete]
        public App()
        {
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
