using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet5.Entities
{
    public class Paciente : Usuario 
    {
        [Required]
        public string NSS { get; set; }

        [Required]
        public string NumTarjeta { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Direccion { get; set; }

        public ICollection<Medico> Medicos { get; set; }

        public ICollection<Cita> Citas { get; set; }

        public Paciente()
        {
            Medicos = new List<Medico>();
            Citas = new List<Cita>();
        }

    }
}
