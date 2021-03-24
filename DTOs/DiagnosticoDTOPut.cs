using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.DTOs
{
    public class DiagnosticoDTOPut
    {
        public int DiagnosticoId { get; set; }
        public string ValoracionEspecialista { get; set; }
        public string Enfermedad { get; set; }
        public int CitaId { get; set; }
    }
}
