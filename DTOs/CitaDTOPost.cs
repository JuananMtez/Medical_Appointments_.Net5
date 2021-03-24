using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.DTOs
{
    public class CitaDTOPost
    {

        public DateTime FechaHora { get; set; }
        public string MotivoCita { get; set; }

        public int PacienteId { get; set; }

        public int MedicoId { get; set; }
    }
}
