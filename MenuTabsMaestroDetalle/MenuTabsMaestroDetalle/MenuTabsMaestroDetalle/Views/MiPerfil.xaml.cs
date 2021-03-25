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
            ListFinal = new List<clsRegistrado>();

            Listregistrado = new List<Registrado>();

            Listregistrado = await App.Database.GetRegistrado();

            if (Listregistrado.Count > 0)
            {

                for (int i = 0; i < Listregistrado.Count; i++)
                {
                    ListFinal.Add(new clsRegistrado
                    {
                        Nombres = Listregistrado[i].Nombres,
                        Apellidos = Listregistrado[i].Apellidos,
                        RegRutaImagen = CreateImage(Listregistrado[i].Imagen)
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
    }
}