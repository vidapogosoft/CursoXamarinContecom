using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace MenuTabsMaestroDetalle.Model
{
    public class clsRegistrado
    {
        public int IdRegistrado { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public ImageSource RegRutaImagen { get; set; }

        public string DescripcionTrabajo { get; set; }
    }
}
