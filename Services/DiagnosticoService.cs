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
    public class DiagnosticoService : IDiagnosticoService
    {

        private readonly MyDbContext context;
        private readonly IMapper mapper;

        public DiagnosticoService (MyDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<ActionResult<IEnumerable<DiagnosticoDTOResponse>>> GetAll()
        {
            IEnumerable<Diagnostico> diagnosticos = await context.Diagnosticos.Include(d => d.Cita).ToListAsync();

            return diagnosticos.Select(d => MapToDTO(d)).ToList();
        }

        public async Task<DiagnosticoDTOResponse> Get(int id)
        {
            return MapToDTO(await context.Diagnosticos.SingleAsync(d => d.DiagnosticoId == id));
        }

        public async Task<int> Put(int id, DiagnosticoDTOPut diagnosticoDTO)
        {
    
            if (id != diagnosticoDTO.DiagnosticoId)
            {
                return 0;
            }

            if (!await context.Diagnosticos.AnyAsync(d => d.DiagnosticoId == id))
            {
                return 1;
            }


            
            if (!context.Diagnosticos.Where(d => d.DiagnosticoId == id  && d.CitaId == diagnosticoDTO.CitaId).Any())
            {
                return 0;
            }

            Diagnostico diagnostico = MapToEntity(diagnosticoDTO);

            context.Entry(diagnostico).State = EntityState.Modified;

            await context.SaveChangesAsync();
            
            return 2;
        }

        public async Task<DiagnosticoDTOResponse> Post(DiagnosticoDTOPost diagnosticoDTO)
        {

            Diagnostico diagnostico = MapToEntity(diagnosticoDTO);

            Cita cita = await context.Citas.Include(c => c.Diagnostico).Include(c => c.Paciente).Include(c => c.Medico).SingleAsync(c => c.CitaId == diagnosticoDTO.CitaId);
            
            if (cita == null)
            {
                return null;
            }


            cita.Diagnostico = diagnostico;
            context.Entry(cita).State = EntityState.Modified;

            diagnostico.Cita = cita;
            diagnostico.CitaId = cita.CitaId;

            context.Diagnosticos.Add(diagnostico);

            await context.SaveChangesAsync();

            DiagnosticoDTOResponse response = MapToDTO(diagnostico);
            return response;
        }



        public async Task<Boolean> Delete(int id)
        {
            var diagnostico = await context.Diagnosticos.FindAsync(id);
            if (diagnostico == null)
            {
                return false;
            }

            context.Diagnosticos.Remove(diagnostico);
            await context.SaveChangesAsync();
            return true;

        }

        private Diagnostico MapToEntity(DiagnosticoDTOPost diagnosticoRequest)
        {
            Diagnostico diagnostico = mapper.Map<Diagnostico>(diagnosticoRequest);
            return diagnostico;
        }

        private Diagnostico MapToEntity(DiagnosticoDTOPut diagnosticoRequest)
        {
            Diagnostico diagnostico = mapper.Map<Diagnostico>(diagnosticoRequest);
            return diagnostico;
        }

        private DiagnosticoDTOResponse MapToDTO(Diagnostico diagnostico)
        {
            DiagnosticoDTOResponse response = mapper.Map<DiagnosticoDTOResponse>(diagnostico);
            return response;
        }

    }
}
