using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DetalleTelefono.Entidades
{
    public class Personas
    {
        [Key]
        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }

        [ForeignKey("PersonaId")]
        public virtual List<TelefonosDetalle> Detalles { get; set; } = new List<TelefonosDetalle>();

        public Personas()
        {
            PersonaId = 0;
            Nombre = string.Empty;
            Cedula = string.Empty;
            Direccion = string.Empty;
            FechaNacimiento = DateTime.Now;
        }

        public Personas(int personaId, string nombre, string cedula, string direccion, DateTime fechaNacimiento, List<TelefonosDetalle> detalles)
        {
            PersonaId = personaId;
            Nombre = nombre;
            Cedula = cedula;
            Direccion = direccion;
            FechaNacimiento = fechaNacimiento;
            Detalles = new List<TelefonosDetalle>();
        }
    }
}
