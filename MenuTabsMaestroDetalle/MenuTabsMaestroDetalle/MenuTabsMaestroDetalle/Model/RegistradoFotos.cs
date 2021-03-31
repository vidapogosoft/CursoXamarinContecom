using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace MenuTabsMaestroDetalle.Model
{
    public class RegistradoFotos
    {

        [PrimaryKey, AutoIncrement]
        public int IdRegistradoFoto { get; set; }

        public int IdRegistrado { get; set; }
        public string Descripcion { get; set; }

        public DateTime FechaRegistro { get; set; }

        public byte[] Imagen { get; set; }


    }
}
