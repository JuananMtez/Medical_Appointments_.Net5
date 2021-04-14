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
    public class MedicoService : IMedicoService
    {

        private readonly MyDbContext context;
        private readonly IMapper mapper;

        public MedicoService(MyDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<MedicoDTOResponse>>> GetAll()
        {


            IEnumerable<Medico> medicos = await context.Medicos.Include(m => m.Pacientes).Include(m => m.Citas).ToListAsync();

            return medicos.Select(m => MapToDTO(m)).ToList();
        }

        public async Task<MedicoDTOResponse> Get(int id)
        {


            if (await context.Medicos.Where(m => m.UsuarioId == id).AnyAsync())
            {
                return MapToDTO(await context.Medicos.Include(m => m.Pacientes).Include(m => m.Citas ).SingleAsync(m => m.UsuarioId == id));
            }
            else
            {
                return null;
            }
        }

        public async Task<ActionResult<IEnumerable<MedicoDTOResponse>>> GetAllMedicosPaciente(int idUsuario)
        {

            Paciente paciente = await context.Pacientes.Include(p => p.Medicos).Include(p => p.Citas).SingleAsync(p => p.UsuarioId == idUsuario);

            List<Medico> medicos = paciente.Medicos.ToList();

            return medicos.Select(m => MapToDTO(m)).ToList();

        }


        public async Task<int> Put(int id, MedicoDTOPut medicoDTO)
        {
            if (id != medicoDTO.UsuarioId)
            {
                return 0;
            }

            if (!await context.Medicos.AnyAsync(p => p.UsuarioId == id))
            {
                return 1;
            }


            foreach (int idPaciente in medicoDTO.PacienteUsuarioId)
            {
                if (!await context.Pacientes.AnyAsync(m => m.UsuarioId == idPaciente))
                {
                    return 0;
                }

            }


            if (medicoDTO.CitasCitaId != null)
            {
                foreach (int idCita in medicoDTO.CitasCitaId)
                {
                    if (!await context.Citas.Where(c => c.CitaId == idCita && c.Medico.UsuarioId == medicoDTO.UsuarioId).AnyAsync())
                    {
                        return 0;
                    }

                }
            }



            Medico medico = await context.Medicos.Include(m => m.Pacientes).Include(m => m.Citas).ThenInclude(c => c.Diagnostico).SingleAsync(m => m.UsuarioId == medicoDTO.UsuarioId);


            medico.UsuarioId = medicoDTO.UsuarioId;
            medico.Nombre = medicoDTO.Nombre;
            medico.Apellidos = medicoDTO.Apellidos;
            medico.User = medicoDTO.User;
            medico.Clave = medicoDTO.Clave;
            medico.NumColegiado = medicoDTO.NumColegiado;


            foreach (int idPaciente in medicoDTO.PacienteUsuarioId)
            {

                if (!medico.Pacientes.Where(p => p.UsuarioId == idPaciente).Any())
                {
                    Paciente paciente = await context.Pacientes.Include(p => p.Medicos).Include(p => p.Citas).ThenInclude(c => c.Diagnostico).SingleAsync(p => p.UsuarioId == idPaciente);

                    paciente.Medicos.Add(medico);
                    context.Entry(paciente).State = EntityState.Modified;

                    medico.Pacientes.Add(paciente);


                }

            }


            foreach (Paciente paciente in medico.Pacientes.ToList())
            {
                bool encontrado = false;
                foreach (int idPaciente in medicoDTO.PacienteUsuarioId)
                {
                    if (paciente.UsuarioId == idPaciente)
                    {
                        encontrado = true;
                    }

                }

                if (!encontrado)
                {
                    medico.Pacientes.Remove(paciente);

                    paciente.Medicos.Remove(medico);
                    context.Entry(paciente).State = EntityState.Modified;
                }




            }

            context.Entry(medico).State = EntityState.Modified;

            await context.SaveChangesAsync();
            return 2;



        }


        public async Task<MedicoDTOResponse> Login(LoginDTO loginForm)
        {

            if (await context.Medicos.Where(m => m.User == loginForm.Username && m.Clave == loginForm.Password).AnyAsync())
            {
                return MapToDTO(await context.Medicos.Include(m => m.Pacientes).Include(m => m.Citas).SingleAsync(m => m.User == loginForm.Username && m.Clave == loginForm.Password));
            }
            else
            {
                return null;
            }
        }

        public async Task<MedicoDTOResponse> Post(MedicoDTOPost medicoDTO)
        {

            if (await context.Usuarios.Where(u => u.User == medicoDTO.User).AnyAsync())
            {
                return null;
            }
            Medico medico = MapToEntity(medicoDTO);


            context.Medicos.Add(medico);
            await context.SaveChangesAsync();

            MedicoDTOResponse response = MapToDTO(medico);

            return response;
        }

        public async Task<Boolean> Delete(int id)
        {
            var medico = await context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return false;
            }

            context.Medicos.Remove(medico);
            await context.SaveChangesAsync();
            return true;

        }

        private bool MedicoExists(int id)
        {
            return context.Pacientes.Any(e => e.UsuarioId == id);
        }


        private MedicoDTOResponse MapToDTO(Medico medico)
        {
            MedicoDTOResponse medicoDTO = mapper.Map<MedicoDTOResponse>(medico);

            if (medico.Pacientes is not null)
            {
                medicoDTO.PacienteUsuarioId = medico.Pacientes.Select(p => p.UsuarioId).ToList();
            }


            if (medico.Citas is not null)
            {
                medicoDTO.CitasCitaId = medico.Citas.Select(m => m.CitaId).ToList();
            }

            return medicoDTO;
        }

        private Medico MapToEntity(MedicoDTOPost medicoRequest)
        {
            Medico medico = mapper.Map<Medico>(medicoRequest);
            return medico;
        }
    }
}
