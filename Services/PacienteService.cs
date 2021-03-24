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
    public class PacienteService : IPacienteService
    {
        private readonly MyDbContext context;
        private readonly IMapper mapper;

        public PacienteService(MyDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<PacienteDTOResponse>>> GetAll()
        {


            IEnumerable<Paciente> pacientes = await context.Pacientes.Include(p => p.Medicos).Include(c => c.Citas).ToListAsync();

            return pacientes.Select(p => MapToDTO(p)).ToList();
        }

        public async Task<PacienteDTOResponse> Get(int id)
        {

            if (await context.Pacientes.Where(p => p.UsuarioId == id).AnyAsync())
            {
                return MapToDTO(await context.Pacientes.Include(p => p.Medicos).Include(p => p.Citas).SingleAsync(p => p.UsuarioId == id));
            }
            else
            {
                return null;
            }
        }

        public async Task<int> Put(int id, PacienteDTOPut pacienteDTO)
        {


            if (id != pacienteDTO.UsuarioId)
            {
                return 0;
            }

            if (!await context.Pacientes.AnyAsync(p => p.UsuarioId == id))
            {
                return 1;
            }


            foreach (int idMedico in pacienteDTO.MedicosUsuarioId)
            {
                if (!await context.Medicos.AnyAsync(m => m.UsuarioId == idMedico)) 
                {
                    return 0;
                }

            }

 

            foreach (int idCita in pacienteDTO.CitasCitaId)
            {
                if (!await context.Citas.Where(c => c.CitaId == idCita && c.Paciente.UsuarioId == pacienteDTO.UsuarioId).AnyAsync())
                {
                    return 0;
                }

            }

            Paciente paciente = await context.Pacientes.Include(p => p.Medicos).Include(p => p.Citas).ThenInclude(c => c.Diagnostico).SingleAsync(p => p.UsuarioId == pacienteDTO.UsuarioId);


            paciente.UsuarioId = pacienteDTO.UsuarioId;
            paciente.Nombre = pacienteDTO.Nombre;
            paciente.Apellidos = pacienteDTO.Apellidos;
            paciente.User = pacienteDTO.User;
            paciente.Clave = pacienteDTO.Clave;
            paciente.NumTarjeta = pacienteDTO.NumTarjeta;
            paciente.Direccion = pacienteDTO.Direccion;


            foreach (int idMedico in pacienteDTO.MedicosUsuarioId)
            {

                if (!paciente.Medicos.Where(m => m.UsuarioId == idMedico).Any())
                {
                    Medico medico = await context.Medicos.Include(m => m.Pacientes).Include(m => m.Citas).ThenInclude(c => c.Diagnostico).SingleAsync(m => m.UsuarioId == idMedico);

                    medico.Pacientes.Add(paciente);
                    context.Entry(medico).State = EntityState.Modified;

                    paciente.Medicos.Add(medico);


                }

            }


            foreach (Medico medico in paciente.Medicos.ToList())
            {
                bool encontrado = false;
                foreach (int idMedico in pacienteDTO.MedicosUsuarioId)
                {
                    if (medico.UsuarioId == idMedico)
                    {
                        encontrado = true;
                    }

                }

                if (!encontrado)
                {
                    paciente.Medicos.Remove(medico);
                   
                    medico.Pacientes.Remove(paciente);
                    context.Entry(medico).State = EntityState.Modified;
                }




            }

            context.Entry(paciente).State = EntityState.Modified;

            await context.SaveChangesAsync();
            return 2;
        }





        public async Task<PacienteDTOResponse> Post(PacienteDTOPost pacienteDTO)
        {


            if (await context.Usuarios.Where(u => u.User == pacienteDTO.User).AnyAsync())
            {
                return null;
            }


            Paciente paciente = MapToEntity(pacienteDTO);




            foreach (int id in pacienteDTO.MedicosUsuarioId)
            {
                Medico medico = await context.Medicos.Include(m => m.Pacientes).SingleAsync(m => m.UsuarioId == id);

                if (medico is not null)
                {
                    medico.Pacientes.Add(paciente);
                    paciente.Medicos.Add(medico);
                    context.Entry(medico).State = EntityState.Modified;
                } else
                {
                    return null;
                }
            }



            context.Pacientes.Add(paciente);
            await context.SaveChangesAsync();

            PacienteDTOResponse response = MapToDTO(paciente);

            return response;
        }



        public async Task<Boolean> Delete(int id)
        {
            var paciente = await context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return false;
            }

            context.Pacientes.Remove(paciente);
            await context.SaveChangesAsync();
            return true;

        }
        public async Task<PacienteDTOResponse> Login(LoginDTO loginForm)
        {

            if (await context.Pacientes.Where(p => p.User == loginForm.Username && p.Clave == loginForm.Password).AnyAsync())
            {
                return MapToDTO(await context.Pacientes.Include(p => p.Medicos).Include(p => p.Citas).SingleAsync(p => p.User == loginForm.Username && p.Clave == loginForm.Password));
            }
            else
            {
                return null;
            }
        }

        private PacienteDTOResponse MapToDTO(Paciente paciente)
        {

            PacienteDTOResponse pacienteDTO = mapper.Map<PacienteDTOResponse>(paciente);

            if (paciente.Medicos is not null)
            {
                pacienteDTO.MedicosUsuarioId = paciente.Medicos.Select(m => m.UsuarioId).ToList();
            }

            if (paciente.Citas is not null)
            {
                pacienteDTO.CitasCitasId = paciente.Citas.Select(c => c.CitaId).ToList();

            }

            return pacienteDTO;
        }



        private Paciente MapToEntity(PacienteDTOPost pacienteDTO)
        {
            Paciente paciente = mapper.Map<Paciente>(pacienteDTO);
            return paciente;


        }


        private Paciente MapToEntityPut(PacienteDTOPut pacienteDTO)
        {
            Paciente paciente = mapper.Map<Paciente>(pacienteDTO);
            return paciente;

        }


    }
}
