using AutoMapper;
using CitasMedicas.DTOs;
using CitasMedicas.Services;
using dotnet5.DTOs;
using dotnet5.Entities;
using dotnet5.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet5.Services
{
    public class CitaService : ICitaService
    {

        private readonly MyDbContext context;
        private readonly IMapper mapper;

        public CitaService(MyDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<ActionResult<IEnumerable<CitaDTOResponse>>> GetAll()
        {
            IEnumerable<Cita> citas = await context.Citas.Include(c => c.Paciente).Include(c => c.Medico).Include(c => c.Diagnostico).ToListAsync();
            return citas.Select(c => MapToDTO(c)).ToList();
        }

        public async Task<CitaDTOResponse> Get(int id)
        {


            return MapToDTO(await context.Citas.Include(c => c.Paciente).Include(c => c.Medico).Include(c => c.Diagnostico).SingleAsync(c => c.CitaId == id));
        }

        public async Task<int> Put(int id, CitaDTOPut citaDTO)
        {
            if (id != citaDTO.CitaId)
            {
                return 0;
            }

            if (!await context.Citas.AnyAsync(c => c.CitaId == id))
            {
                return 1;
            }



            if (!await context.Citas.Where(c => c.CitaId == id && c.Paciente.UsuarioId == citaDTO.PacienteUsuarioId && c.Medico.UsuarioId == citaDTO.MedicoUsuarioId && 
            (c.Diagnostico.DiagnosticoId == citaDTO.DiagnosticoDiagnosticoId || citaDTO.DiagnosticoDiagnosticoId == 0)).AnyAsync())
            {
                return 0;
            }


            Cita cita = MapToEntityPut(citaDTO);
            context.Entry(cita).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return 2;




        }

        public async Task<CitaDTOResponse> Post(CitaDTOPost citaDTO)
        {




            Medico medico = await context.Medicos.FindAsync(citaDTO.MedicoUsuarioId);
            Paciente paciente = await context.Pacientes.FindAsync(citaDTO.PacienteUsuarioId);



            Cita cita = MapToEntity(citaDTO);

            paciente.Citas.Add(cita);
            medico.Citas.Add(cita);

            context.Entry(paciente).State = EntityState.Modified;
            context.Entry(medico).State = EntityState.Modified;
            context.Citas.Add(cita);

            await context.SaveChangesAsync();

            CitaDTOResponse response = MapToDTO(cita);
            return response;
        }


        public async Task<Boolean> Delete(int id)
        {
            var cita = await context.Citas.FindAsync(id);
            if (cita == null)
            {
                return false;
            }

            context.Citas.Remove(cita);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<ActionResult<IEnumerable<CitaDTOResponse>>> GetByPaciente(int idUsuario)
        {
            IEnumerable<Cita> citas = await context.Citas.Include(c => c.Paciente).Include(c => c.Medico).Include(c => c.Diagnostico).Where(c => c.Paciente.UsuarioId == idUsuario).ToListAsync();

            return citas.Select(c => MapToDTO(c)).ToList();
        }

        public async Task<ActionResult<IEnumerable<CitaDTOResponse>>> GetByMedico(int idUsuario)
        {
            IEnumerable<Cita> citas = await context.Citas.Include(c => c.Paciente).Include(c => c.Medico).Include(c => c.Diagnostico).Where(c => c.Medico.UsuarioId == idUsuario).ToListAsync();

            return citas.Select(c => MapToDTO(c)).ToList();
        }


        private CitaDTOResponse MapToDTO(Cita cita)
        {
            CitaDTOResponse citaDTO = mapper.Map<CitaDTOResponse>(cita);
            return citaDTO;
        }

        private Cita MapToEntity(CitaDTOPost citaDTO)
        {
            Cita cita = mapper.Map<Cita>(citaDTO);
            return cita;
        }

        private Cita MapToEntityPut(CitaDTOPut citaDTO)
        {
            Cita cita = mapper.Map<Cita>(citaDTO);
            return cita;
        }



    }
}
