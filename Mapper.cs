using AutoMapper;
using CitasMedicas.DTOs;
using dotnet5.DTOs;
using dotnet5.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet5
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UsuarioDTO, Usuario>();
            CreateMap<Usuario, UsuarioDTOResponse>();

            CreateMap<PacienteDTOPost, Paciente>();
            CreateMap<PacienteDTOPut, Paciente>();
            CreateMap<Paciente, PacienteDTOResponse>();

            CreateMap<MedicoDTOPost, Medico>();
            CreateMap<MedicoDTOPut, Medico>();
            CreateMap<Medico, MedicoDTOResponse>();


            CreateMap<CitaDTOPost, Cita>();
            CreateMap<CitaDTOPut, Cita>();
            CreateMap<Cita, CitaDTOResponse>();


            CreateMap<DiagnosticoDTOPost, Diagnostico>();
            CreateMap<DiagnosticoDTOPut, Diagnostico>();
            CreateMap<Diagnostico, DiagnosticoDTOResponse>();

        }
    }
}
