using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppApiRest.Model;

namespace AppApiRest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        public Registro()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            DtFechaSalida.Date = DateTime.Now.Date.AddDays(1);
            DtFechaSalida.MinimumDate = DateTime.Now;

        }

        private async void BtnRegistrarme_Clicked(object sender, EventArgs e)
        {
            int grabadoPerfil;

            var ConfirmaRegistro = new clsPerfilDUO
            {
                
                RegRutaImagen = "https://admin-sysnnova.com/sysnnovasite/images/team/team4.jpg",
                RegPerfilId = 0,
                RegId = int.Parse(IdRegistrado.Text),

                RegProfesion = this.Profesion.Text,
                RegAcercaDe = this.AcercaDe.Text,
                RegNombres = this.Nombres.Text,
                RegApellidos = this.Apellidos.Text,
                RegNombresCompletos = this.Nombres.Text + " " + this.Apellidos.Text,
                RegEmail = this.Email.Text,
                RegNumeroCelular = this.Celular.Text,
                RegCodigoUnico = "0000000000",
                RegFecha = DateTime.Now,
                Tecnologia = Tecno.IsToggled,
                Legales = Legal.IsToggled,
                Comunicacion = Comuni.IsToggled,
                Comercio = Comer.IsToggled,
                ArteDiseno = Arte.IsToggled,
                ServiciosTecnicos = SerTec.IsToggled,
                Urbanismo = Urba.IsToggled,
                Emprendimientos = true
            };

            grabadoPerfil = await App.Manager.SaveRegistro(ConfirmaRegistro, true);
            
            if (grabadoPerfil == 1)
            {
                await DisplayAlert("Registro", "Tus dastos de perfil DUO han sido registrados con exito.", "De Acuerdo");

                await Navigation.PopAsync();
            }

        }
    }
}