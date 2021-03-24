using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet5.Entities;
using dotnet5.Repositories;
using dotnet5.Services;
using dotnet5.DTOs;
using CitasMedicas.DTOs;
using CitasMedicas.Services;

namespace dotnet5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTOResponse>>> GetUsuarios()
        {
            
            return await usuarioService.GetAll();

        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTOResponse>> GetUsuario(int id)
        {
            var usuario = await usuarioService.Get(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }


        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioDTO usuarioDTO)
        {

            switch (await usuarioService.Put(id, usuarioDTO))
            {
                case 0:
                    return BadRequest();

                case 1:
                    return NotFound();

                case 2: return NoContent();

            }
            return null;
        }

  
        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (await usuarioService.Delete(id))
            {
                return NoContent();
            }

            return NotFound();


        }
    }
}