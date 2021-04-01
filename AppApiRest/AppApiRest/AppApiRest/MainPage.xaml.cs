using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using AppApiRest.Model;

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

     }
}
