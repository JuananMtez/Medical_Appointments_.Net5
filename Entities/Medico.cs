using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet5.Entities
{
    public class Medico : Usuario
    {

        [Required]
        public string NumColegiado { get; set; }

        public ICollection<Paciente> Pacientes { get; set; }

        public ICollection<Cita> Citas { get; set; }



        public Medico()
        {
            Pacientes = new List<Paciente>();
            Citas = new List<Cita>();

        }






    }
}
