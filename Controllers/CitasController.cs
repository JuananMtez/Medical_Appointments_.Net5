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
using Microsoft.AspNetCore.Cors;

namespace dotnet5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {

        private readonly ICitaService citaService;

        public CitasController(ICitaService citaService)
        {
            this.citaService = citaService;
        }

        // GET: api/Citas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaDTOResponse>>> GetCitas()
        {
            return await citaService.GetAll();
        }

        // GET: api/Citas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CitaDTOResponse>> GetCita(int id)
        {
            var cita = await citaService.Get(id);
            if (cita == null)
            {
                return NotFound();
            }
            return cita;
        }

        // PUT: api/Citas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, CitaDTOPut citaDTO)
        {

            switch (await citaService.Put(id, citaDTO))
            {
                case 0:
                    return BadRequest();

                case 1:
                    return NotFound();

                case 2: return NoContent();

            }
            return null;
        }

        // POST: api/Citas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CitaDTOResponse>> PostCita(CitaDTOPost citaDTO)
        {
            CitaDTOResponse response = await citaService.Post(citaDTO);
            if (response == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetCita", new { id = response.CitaId }, response);
        }

        // DELETE: api/Citas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {

            if (await citaService.Delete(id))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
