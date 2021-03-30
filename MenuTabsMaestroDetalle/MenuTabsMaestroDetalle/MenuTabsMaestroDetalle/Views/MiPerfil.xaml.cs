using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MenuTabsMaestroDetalle.Model;

using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;

using Xamarin.Forms.Maps;
using Plugin.Geolocator;


namespace MenuTabsMaestroDetalle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiPerfil : TabbedPage
    {

        public List<Registrado> Listregistrado;
        public List<clsRegistrado> ListFinal;

        public byte[] ArrayFoto;  //Almacenar Foto


        public MiPerfil()
        {
            InitializeComponent();
            InitializePluginLocator();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Consultar mis datos
            CargaMisDatos();

        }


        public void MostrarPosMapa(double Latitud , double Longitud)
        {
            base.OnAppearing();

            MiMapa.MoveToRegion(

                MapSpan.FromCenterAndRadius
                (
                       new Position (Latitud, Longitud) ,
                       Distance.FromKilometers(0.5)
                )
                );

            //makers - Pin
           
            var position = new Position(Latitud, Longitud);

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "Curso de Xamarin Forms 5.0",
                Address = "GYE"
            };

            //Asignar los pin al mapa
            if (MiMapa.Pins.Count > 0)
            {
                MiMapa.Pins.Remove(pin);
            }

            MiMapa.Pins.Add(pin);

        }

        private async void InitializePluginLocator()
        {
            try
            {

                if (!CrossGeolocator.IsSupported)
                {
                    await DisplayAlert("Error", "NO habilitado geolocalizacion", "Cerrar");

                    return;
                }

                if (!CrossGeolocator.Current.IsGeolocationEnabled
                    || !CrossGeolocator.Current.IsGeolocationAvailable)
                {

                    await DisplayAlert("Error", "Geolocalizacion restringida", "Cerrar");

                    return;
                }

                CrossGeolocator.Current.PositionChanged += Current_PositionChanged;

                //Espcifico el tiempo con que se actualizan las coordenadas

                await CrossGeolocator.Current.StartListeningAsync(new TimeSpan(0,0,3), 0.5);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Cerrar");
            }

        }

        private void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {

            if(!CrossGeolocator.Current.IsListening)
            {
                DisplayAlert("Advertencia", "No se esta visualizando las coordenadas", "Cerrar");
                return;
            }

            var position = CrossGeolocator.Current.GetPositionAsync();

            Latitud.Text = position.Result.Latitude.ToString();
            Longitud.Text = position.Result.Longitude.ToString();
            Altimetria.Text = position.Result.Altitude.ToString();

            //Muestro en mapa la posicion Geo - Localizada

            MostrarPosMapa(position.Result.Latitude, position.Result.Longitude);

        }

      
        public async void CargaMisDatos()
        {
            Listregistrado = new List<Registrado>();
            
            ListFinal = new List<clsRegistrado>();

            Listregistrado = await App.Database.GetRegistrado();

            if (Listregistrado.Count > 0)
            {

                for (int i = 0; i < Listregistrado.Count; i++)
                {
                    ListFinal.Add(

                        new clsRegistrado
                        {
                            IdRegistrado = Listregistrado[i].IdRegistrado,
                            Nombres = Listregistrado[i].Nombres,
                            Apellidos = Listregistrado[i].Apellidos,
                            RegRutaImagen =  CreateImage(Listregistrado[i].Imagen)
                        });
                }

                //CVDatos.ItemsSource = Listregistrado;

                CVDatos.ItemsSource = ListFinal;
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
                        FechaRegistro = DateTime.Now,
                        Imagen = ArrayFoto
                    };

                    grabado = await App.Database.RegistroDatos(Confirmar);

                    if (grabado == 1)
                    {
                        TxtNombres.Text = string.Empty;
                        TxtApellidos.Text = string.Empty;
                        MainImage.Source = "icon.png";
                        Path.Text = string.Empty;

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

        private async void BtnFoto_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("¿Que deseas realizar?", "Cancelar", null,
                "Tomar Foto", "Subir Foto");


            if (action == "Tomar Foto")
            {
                Camara(1);
            }

            if (action == "Subir Foto")
            {
                Camara(2);
            }

        }

        private async void Camara(int Accion)
        {
            try
            {

                MediaFile file = null;

                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable

                    || !CrossMedia.Current.IsTakePhotoSupported

                    || !CrossMedia.Current.IsPickPhotoSupported

                    )
                {

                    await DisplayAlert("Camara no habilitada", "Revise su dispositivo", "Cerrar");

                    return;
                
                }


                if (Accion == 1)
                {
                   file = await CrossMedia.Current.TakePhotoAsync(

                        new StoreCameraMediaOptions
                        {

                            SaveToAlbum = true,
                            PhotoSize = PhotoSize.Custom,
                            CustomPhotoSize = 70
                        }
                        ) ;
                }


                if (Accion == 2)
                {
                   file = await CrossMedia.Current.PickPhotoAsync();

                }


                if (file == null)
                {
                    await DisplayAlert("Camara", "No realizo nada con la camara", "Cerrar");
                    return;
                }

                this.Path.Text = file.AlbumPath;

                this.MainImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();

                    ArrayFoto = ReadImage(file.GetStream());

                    file.Dispose();
                    return stream;
                }

               );


            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message, "Cerrar");
            }
        }


        public byte[] ReadImage(Stream Imput)
        {
            BinaryReader reader = new BinaryReader(Imput);
            byte[] imgByte = reader.ReadBytes((int)Imput.Length);

            return imgByte;

        }


        public ImageSource CreateImage(byte[] input)
        {
            Stream streamr;

            Image image = new Image();
            streamr = new MemoryStream(input);
            return image.Source = ImageSource.FromStream(() =>
            {
                return streamr;

            });

        }

        private async void CVDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.CurrentSelection.Any())
            {
                //ImageSource FotoRegistrada;
                string Nombres, Apellidos, IdRegistrado;

                Nombres = (CVDatos.SelectedItem as clsRegistrado)?.Nombres;
                Apellidos = (CVDatos.SelectedItem as clsRegistrado)?.Apellidos;
                IdRegistrado = (CVDatos.SelectedItem as clsRegistrado)?.IdRegistrado.ToString();
                //FotoRegistrada = (CVDatos.SelectedItem as clsRegistrado)?.RegRutaImagen;


                TxtNombres.Text = Nombres;
                TxtApellidos.Text = Apellidos;


                //Consultar imagen a la base

                Listregistrado = new List<Registrado>();

                Listregistrado = await App.Database.GetRegistradoById(int.Parse(IdRegistrado));


                if (Listregistrado.Count > 0)
                {

                    for (int i = 0; i < Listregistrado.Count; i++)
                    {
                        ArrayFoto = Listregistrado[i].Imagen;
                        MainImage.Source = CreateImage(Listregistrado[i].Imagen);
                        LblIdRegistrado.Text = Listregistrado[i].IdRegistrado.ToString();
                    }

                }

                  CVDatos.SelectedItem = null;

            }

        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            bool DeACuerdo;
            int grabado;

            DeACuerdo = await DisplayAlert("Confirmar", "¿Desea actualizar datos?", "De Acuerdo", "Cancelar");


            if (DeACuerdo)
            {

                var Actualizar = new Registrado
                {
                    IdRegistrado =  int.Parse(LblIdRegistrado.Text),
                    Nombres = TxtNombres.Text,
                    Apellidos = TxtApellidos.Text,
                    FechaRegistro = DateTime.Now,
                    Imagen = ArrayFoto
                };

                grabado = await App.Database.UpdateDatos(Actualizar);

                if (grabado == 1)
                {
                    await DisplayAlert("Exito!", "Datos Actualizados", "De Acuerdo");

                    LblIdRegistrado.Text = "0";
                    TxtNombres.Text = string.Empty;
                    TxtApellidos.Text = string.Empty;
                    MainImage.Source = "icon.png";
                    Path.Text = string.Empty;

                    CargaMisDatos();

                }
            }
        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {

            bool DeACuerdo;
            int grabado;

            DeACuerdo = await DisplayAlert("Confirmar", "¿Desea eliminar datos?", "De Acuerdo", "Cancelar");


            if (DeACuerdo)
            {

                var Borrar = new Registrado
                {
                    IdRegistrado = int.Parse(LblIdRegistrado.Text),
                    Nombres = TxtNombres.Text,
                    Apellidos = TxtApellidos.Text,
                    FechaRegistro = DateTime.Now,
                    Imagen = ArrayFoto
                };

                grabado = await App.Database.DeleteDatos(Borrar);

                if (grabado == 1)
                {
                    await DisplayAlert("Exito!", "Datos Eliminados", "De Acuerdo");

                    LblIdRegistrado.Text = "0";
                    TxtNombres.Text = string.Empty;
                    TxtApellidos.Text = string.Empty;
                    MainImage.Source = "icon.png";
                    Path.Text = string.Empty;

                    CargaMisDatos();

                }
            }

        }
    }
}