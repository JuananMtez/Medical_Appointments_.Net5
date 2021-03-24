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
    public class UsuarioService : IUsuarioService
    {
        private readonly  MyDbContext context;
        private readonly IMapper mapper;

        public UsuarioService(MyDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<UsuarioDTOResponse>>> GetAll()
        {
            
            IEnumerable<Usuario> usuarios = await context.Usuarios.ToListAsync();
            return usuarios.Select(u => MapToDTO(u)).ToList();

        }
        
        public async Task<UsuarioDTOResponse> Get(int id)
        {
            return MapToDTO(await context.Usuarios.FindAsync(id));
        }

        public async Task<int> Put(int id, UsuarioDTO usuarioDTO)
        {


            if (id != usuarioDTO.UsuarioId)
            {
                return 0;
            }
            if (!await context.Usuarios.AnyAsync(u => u.UsuarioId == id))
            {
                return 1;
            }


            Usuario usuario = MapToEntity(usuarioDTO);


            context.Entry(usuario).State = EntityState.Modified;
            
           
            await context.SaveChangesAsync();
            return 2;
            

        }



        public async Task<Boolean> Delete(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return false;
            }

            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();
            return true;
        }


        public UsuarioDTOResponse MapToDTO(Usuario usuario)
        {
            UsuarioDTOResponse response = mapper.Map<UsuarioDTOResponse>(usuario);
            return response;
        }


        public Usuario MapToEntity(UsuarioDTO usuarioDTO)
        {
            Usuario usuario = mapper.Map<Usuario>(usuarioDTO);
            return usuario;
        }
    }

}
