using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MenuTabsMaestroDetalle.Model;

namespace MenuTabsMaestroDetalle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiPerfil : TabbedPage
    {

        public List<Registrado> Listregistrado;

        public MiPerfil()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Consultar mis datos
            CargaMisDatos();

        }

        public async void CargaMisDatos()
        {
            Listregistrado = new List<Registrado>();

            Listregistrado = await App.Database.GetRegistrado();

            if (Listregistrado.Count > 0)
            {
                CVDatos.ItemsSource = Listregistrado;
            }


        }

        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool DeACuerdo;
                int grabado;

                DeACuerdo = await DisplayAlert("Confirmar", "¿Desea registrase?", "De Acuerdo", "Cancelar");


                if (DeACuerdo)
                {

                    var Confirmar = new Registrado
                    {
                        Nombres = TxtNombres.Text,
                        Apellidos = TxtApellidos.Text,
                        FechaRegistro = DateTime.Now
                    };

                    grabado = await App.Database.RegistroDatos(Confirmar);

                    if (grabado == 1)
                    {
                        TxtNombres.Text = string.Empty;
                        TxtApellidos.Text = string.Empty;

                        CargaMisDatos();
                        await DisplayAlert("Exito!", "Bienvenido", "De Acuerdo");

                    }
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Cerrar");
            }
        }
    }
}