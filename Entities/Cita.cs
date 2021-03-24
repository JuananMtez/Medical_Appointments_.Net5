using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet5.Entities
{
    public class Cita
    {

        public int CitaId { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

        [Required]
        public string MotivoCita { get; set; }
        
        [Required]
        public Paciente Paciente { get; set; }

        [Required]
        public Medico Medico { get; set; }

        public Diagnostico Diagnostico { get; set; }


    }
}
