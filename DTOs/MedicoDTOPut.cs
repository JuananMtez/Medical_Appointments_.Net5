using dotnet5.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.DTOs
{
    public class MedicoDTOPut
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string User { get; set; }
        public string Clave { get; set; }
        public string NumColegiado {get; set; }

        public List<int> PacienteUsuarioId { get; set; }

        public List<int> CitasCitaId { get; set; }




    }
}
