using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet5.DTOs
{
    public class CitaDTOResponse
    {

		public int CitaId { get; set; }
		public string FechaHora { get; set; }
		public int PacienteUsuarioId { get; set; }
		public string PacienteNombre { get; set; }
		public string PacienteApellidos { get; set; }

		public int MedicoUsuarioId { get; set; }
		public string MedicoNombre { get; set; }
		public string MedicoApellidos { get; set; }
		public string MotivoCita { get; set; }

		public string DiagnosticoDiagnosticoId { get; set; }
	}
}
