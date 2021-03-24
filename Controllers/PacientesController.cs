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
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService pacienteService;


        public PacientesController(IPacienteService pacienteService)
        {
            this.pacienteService = pacienteService;
        }

        // GET: api/Pacientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteDTOResponse>>> GetPacientes()
        {
            return await pacienteService.GetAll();
        }

        // GET: api/Pacientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDTOResponse>> GetPaciente(int id)
        {
            var paciente = await pacienteService.Get(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return paciente;
        }

        // PUT: api/Pacientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, PacienteDTOPut pacienteDTOPut)
        {
            switch (await pacienteService.Put(id, pacienteDTOPut))
            {
                case 0:
                    return BadRequest();

                case 1:
                    return NotFound();

                case 2: return NoContent();

            }
            return null;
        }

        // POST: api/Pacientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PacienteDTOResponse>> PostPaciente(PacienteDTOPost pacienteDTO)
        {
            PacienteDTOResponse pacienteResponse = await pacienteService.Post(pacienteDTO);

            if (pacienteResponse == null)
            {
                return Conflict();
            }
            return CreatedAtAction("GetPaciente", new { id = pacienteResponse.UsuarioId }, pacienteResponse);
        }



        // POST: api/Pacientes/login
        [HttpPost("login")]
        public async Task<ActionResult<PacienteDTOResponse>> LoginPaciente(LoginDTO loginForm)
        {

            PacienteDTOResponse response = await pacienteService.Login(loginForm);

            if (response != null)
            {
                return response;
            }

            return NotFound();


        }

        // DELETE: api/Pacientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            if (await pacienteService.Delete(id))
            {
                return NoContent();
            }

            return NotFound();
        }

    }
}
