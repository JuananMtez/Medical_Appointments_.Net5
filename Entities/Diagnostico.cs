using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet5.Entities
{
    public class Diagnostico
    {
        public int DiagnosticoId { get; set; }

        [Required]
        public string ValoracionEspecialista { get; set; }

        [Required]
        public string Enfermedad { get; set; }
               
        public int CitaId { get; set; }
        public Cita Cita { get; set; }

    }
}
