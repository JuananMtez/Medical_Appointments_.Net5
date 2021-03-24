using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.DTOs
{
    public class CitaDTOPut
    {

        public int CitaId { get; set; }
        public DateTime FechaHora { get; set; }
        public string MotivoCita { get; set; }

        public int PacienteUsuarioId { get; set; }

        public int MedicoUsuarioId { get; set; }
        public int DiagnosticoDiagnosticoId { get; set; }
    }
}
