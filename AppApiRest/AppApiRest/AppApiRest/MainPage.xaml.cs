using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using AppApiRest.Model;

using Plugin.LocalNotifications;

namespace AppApiRest
{
    public partial class MainPage : ContentPage
    {
        public List<clsRegistrados> ListRegistrados;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            CrossLocalNotifications.Current.Show("No olvidar", "Notificacion con delay",
                0, DateTime.Now.AddSeconds(3));


            Device.StartTimer( TimeSpan.FromSeconds(10), (Func<bool>)(() =>
                {

                    CrossLocalNotifications.Current.Show("No olvidar", "Notificacion con  Device Starter" 
                        + DateTime.Now.ToLongTimeString());

                    return true;
                }));

                

            CargarRegistrados();

        }

        private async void CargarRegistrados()
        {
            ListRegistrados = new List<clsRegistrados>();


            ListRegistrados = await App.Manager.GetApiRegistrados();

            if( ListRegistrados.Count > 0)
            {
                CVRegistrados.ItemsSource = ListRegistrados;
            }

        }

        private async void BtnNuevo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
        }

        private void BtnNotif_Clicked(object sender, EventArgs e)
        {
            CrossLocalNotifications.Current.Show("Titulo de la notificacion", "Cuerpo del mensaje de la notificacion");
        }
    }
}
