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
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoService medicoService;


        public MedicosController(IMedicoService medicoService)
        {
            this.medicoService = medicoService;
        }

        // GET: api/Medicos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicoDTOResponse>>> GetMedicos()
        {
            return await medicoService.GetAll();
        }

        // GET: api/Medicos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoDTOResponse>> GetMedico(int id)
        {
            var medico = await medicoService.Get(id);
            if (medico == null)
            {
                return NotFound();
            }
            return medico;
        }

        // PUT: api/Medicos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedico(int id, MedicoDTOPut medicoDTO)
        {
            switch (await medicoService.Put(id, medicoDTO))
            {
                case 0:
                    return BadRequest();

                case 1:
                    return NotFound();

                case 2: return NoContent();

            }
            return null;
        
        }

        // POST: api/Medicos/login
        [HttpPost("login")]
        public async Task<ActionResult<MedicoDTOResponse>> LoginMedico(LoginDTO loginForm)
        {

            MedicoDTOResponse response = await medicoService.Login(loginForm);

            if (response != null)
            {
                return response;
            }

            return NotFound();


        }



        // POST: api/Medicos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MedicoDTOResponse>> PostMedico(MedicoDTOPost medicoDTO)
        {
            MedicoDTOResponse medico = await medicoService.Post(medicoDTO);
            if (medico == null)
            {
                return Conflict();
            }
            return CreatedAtAction("GetMedico", new { id = medico.UsuarioId }, medico);
        }

        // DELETE: api/Medicos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedico(int id)
        {
            if (await medicoService.Delete(id))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
