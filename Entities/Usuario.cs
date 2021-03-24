using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet5.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string Clave { get; set; }


    }
}
