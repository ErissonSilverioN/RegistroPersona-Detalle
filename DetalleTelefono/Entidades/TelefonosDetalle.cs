using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DetalleTelefono.Entidades
{
    public class TelefonosDetalle
    {
        [Key]
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public string TipoTelefono { get; set; }
        public string Telefono { get; set; }


        public TelefonosDetalle()
        {
           

        }

        public TelefonosDetalle(int id, int personaId, string tipoTelefono, string telefono)
        {
            Id = id;
            PersonaId = personaId;
            TipoTelefono = tipoTelefono;
            Telefono = telefono;
        }
    }
}
