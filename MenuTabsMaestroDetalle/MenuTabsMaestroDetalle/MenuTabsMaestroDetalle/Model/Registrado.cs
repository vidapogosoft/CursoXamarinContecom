using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace MenuTabsMaestroDetalle.Model
{
    
    public class Registrado
    {
        [PrimaryKey, AutoIncrement]
        public int IdRegistrado { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public DateTime FechaRegistro  { get; set; }

        public byte[] Imagen { get; set; }
    }
}
