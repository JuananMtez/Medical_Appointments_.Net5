using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet5.DTOs
{
    public class DiagnosticoDTOResponse
    {
		public string DiagnosticoId { get; set; }
		public string CitaId { get; set; }
		
		public string Enfermedad { get; set; }
		public string ValoracionEspecialista { get; set; }
	}
}
