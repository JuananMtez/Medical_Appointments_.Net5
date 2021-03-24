using CitasMedicas.DTOs;
using dotnet5.DTOs;
using dotnet5.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.Services
{
    public interface IUsuarioService
    {

        public Task<ActionResult<IEnumerable<UsuarioDTOResponse>>> GetAll();

        public Task<UsuarioDTOResponse> Get(int id);

        public Task<int> Put(int id, UsuarioDTO usuarioDTO);


        public Task<Boolean> Delete(int id);


    }

}
