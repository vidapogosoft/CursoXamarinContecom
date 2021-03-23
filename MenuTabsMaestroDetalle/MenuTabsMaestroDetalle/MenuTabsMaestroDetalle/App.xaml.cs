using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MenuTabsMaestroDetalle.Interface;
using MenuTabsMaestroDetalle.DA;


namespace MenuTabsMaestroDetalle
{
    public partial class App : Application
    {
        static Data database;

        public static Data Database
        {
            get {

                if (database == null)
                {
                    database = new Data(DependencyService.Get<ILocHelper>().GetLocalFilePath("database.db"));
                }

                return database;
            
            }

        }


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
