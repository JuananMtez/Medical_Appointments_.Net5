using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet5.DTOs
{
    public class PacienteDTOResponse
    {
		public int UsuarioId { get; set; }
		public string NSS { get; set; }
		public string NumTarjeta { get; set; }
		public string Nombre { get; set; }
		public string Apellidos { get; set; }
		public string User { get; set; }
		public string Clave { get; set; }
		public string Telefono { get; set; }
		public string Direccion { get; set; }

		public List<int> MedicosUsuarioId { get; set; }

		public List<int> CitasCitaId { get; set; }


    }
}
