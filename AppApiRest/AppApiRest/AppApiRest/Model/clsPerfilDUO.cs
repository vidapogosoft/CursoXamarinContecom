using System;
using System.Collections.Generic;
using System.Text;

namespace AppApiRest.Model
{
    public class clsPerfilDUO
    {
        public int RegPerfilId { get; set; }

        public int RegId { get; set; }

        public string RegCodigoUnico { get; set; }
        public string RegRutaImagen { get; set; }
        public byte[] RegStreamImagen { get; set; }

        public string RegNombres { get; set; }

        public string RegApellidos { get; set; }

        public string RegNombresCompletos { get; set; }

        public string RegProfesion { get; set; }

        public string RegAcercaDe { get; set; }

        public string RegEmail { get; set; }

        public string RegNumeroCelular { get; set; }
        public DateTime RegFecha { get; set; }
        public int AnioReg { get; set; }
        public int MesReg { get; set; }
        public int DiaReg { get; set; }
        public bool Tecnologia { get; set; }
        public bool Legales { get; set; }
        public bool Comunicacion { get; set; }
        public bool Comercio { get; set; }
        public bool ArteDiseno { get; set; }
        public bool ServiciosTecnicos { get; set; }
        public bool Urbanismo { get; set; }
        public bool Emprendimientos { get; set; }

    }
}
