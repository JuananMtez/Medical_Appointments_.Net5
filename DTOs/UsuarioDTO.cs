using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.DTOs
{
    public class UsuarioDTO
    {

        public int UsuarioId { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public string User { get; set; }

        public string Clave { get; set; }
    }
}
